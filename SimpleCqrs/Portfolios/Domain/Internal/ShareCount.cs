using System;
using System.Collections.Generic;
using SimpleCqrs.Common.Domain;
using SimpleCqrs.Portfolios.Domain.Failures;

namespace SimpleCqrs.Portfolios.Domain.Internal
{
    internal sealed class ShareCount : ValueObject
    {
        public ShareCount(int count, Symbol symbol)
        {
            if (count < 0) 
                throw new ArgumentException("ShareCount cannot be negative.");
            
            Count = count;
            Symbol = symbol;
        }
        
        public Symbol Symbol { get; }
        private int Count { get; }

        public static ShareCount Zero(Symbol symbol) =>
            new(0, symbol);

        public bool CanDebit(ShareCount shareCount) =>
            Symbol == shareCount.Symbol && Count >= shareCount.Count;

        public ShareCount Debit(ShareCount shares)
        {
            if (!CanDebit(shares)) throw new InsufficientShares(shares, this);
            return new ShareCount(Count - shares.Count, Symbol);
        }

        public int ToInt() => Count;

        public override string ToString() => 
            $"{Count} {Symbol}";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Count;
            yield return Symbol;
        }
    }
}