using System;
using System.Collections.Generic;
using StockTrader.Portfolios.Domain.Failures;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal sealed class ShareCount : ValueObject<ShareCount>
    {
        public ShareCount(int count, Symbol symbol)
        {
            if (count < 0) 
                throw new ArgumentException(
                    "Share count cannot be negative.", nameof(count));
            
            Count = count;
            Symbol = symbol;
        }
        
        public Symbol Symbol { get; }
        private int Count { get; }

        public static ShareCount Zero(Symbol symbol) =>
            new(0, symbol);

        public bool CanDebit(ShareCount shares)
        {
            if (shares.Symbol != Symbol)
                throw new ArgumentException(
                    $"Cannot debit shares of different symbol, {Symbol} expected, {shares.Symbol} received.",
                    nameof(shares));
            
            return Count >= shares.Count;
        }

        public ShareCount Debit(ShareCount shares)
        {
            if (!CanDebit(shares)) throw new InsufficientShares(shares, this);
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
    }
}