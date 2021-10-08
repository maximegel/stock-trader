using System;
using System.Collections.Generic;
using StockTrader.Portfolios.Domain.Failures;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal sealed class ShareCount : ValueObject<ShareCount>,
        IComparable<ShareCount>, IComparable
    {
        public ShareCount(int count, Symbol symbol)
        {
            if (count < 0)
                throw new ArgumentException("Share count cannot be negative.", nameof(count));
            Count = count;
            Symbol = symbol;
        }

        public Symbol Symbol { get; }
        private int Count { get; }

        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is ShareCount other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(ShareCount)}.");
        }

        public int CompareTo(ShareCount? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            if (other.Symbol != Symbol)
                throw new ArgumentException(
                    $"Cannot compare share counts of different symbol, {Symbol} expected, {other.Symbol} received.",
                    nameof(other));
            return Count.CompareTo(other.Count);
        }

        public static ShareCount Zero(Symbol symbol) =>
            new(0, symbol);

        public ShareCount Debit(ShareCount shares)
        {
            if (this < shares) throw new InsufficientShares(this, shares);
            return new ShareCount(Count - shares.Count, Symbol);
        }

        public override string ToString() =>
            $"{Count} {Symbol}";

        public static implicit operator int(ShareCount self) =>
            self.Count;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Count;
            yield return Symbol;
        }

        public static bool operator <(ShareCount? left, ShareCount? right) =>
            Comparer<ShareCount>.Default.Compare(left, right) < 0;

        public static bool operator >(ShareCount? left, ShareCount? right) =>
            Comparer<ShareCount>.Default.Compare(left, right) > 0;

        public static bool operator <=(ShareCount? left, ShareCount? right) =>
            Comparer<ShareCount>.Default.Compare(left, right) <= 0;

        public static bool operator >=(ShareCount? left, ShareCount? right) =>
            Comparer<ShareCount>.Default.Compare(left, right) >= 0;
    }
}