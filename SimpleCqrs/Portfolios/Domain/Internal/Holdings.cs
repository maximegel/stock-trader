using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using SimpleCqrs.Portfolios.Domain.Failures;

namespace SimpleCqrs.Portfolios.Domain.Internal
{
    internal class Holdings : IEnumerable<ShareCount>
    {
        private readonly ImmutableDictionary<Symbol, ShareCount> _items;

        public Holdings(IEnumerable<ShareCount> items) :
            this(items.ToImmutableDictionary(item => item.Symbol)) { }
        
        private Holdings(ImmutableDictionary<Symbol, ShareCount> items) => 
            _items = items;

        public static Holdings Empty { get; } = new(ImmutableDictionary<Symbol, ShareCount>.Empty);

        public ShareCount CountOf(Symbol symbol)
        {
            return NoSharesOf(symbol) 
                ? ShareCount.Zero(symbol) 
                : _items[symbol];
        }

        public bool CanDebit(ShareCount shareCount)
        {
            var symbol = shareCount.Symbol;
            var heldShares = CountOf(symbol);
            return heldShares.CanDebit(shareCount);
        }
        
        public Holdings Debit(ShareCount shareCount)
        {
            var symbol = shareCount.Symbol;
            var heldShares = CountOf(shareCount.Symbol);
            if (!CanDebit(shareCount)) throw new InsufficientShares(shareCount, heldShares);
            return SetCountOf(symbol, shares => shares.Debit(shareCount));
        }

        private bool NoSharesOf(Symbol symbol) =>
            !_items.ContainsKey(symbol);

        private Holdings SetCountOf(Symbol symbol, Func<ShareCount, ShareCount> mutation)
        {
            var shares = CountOf(symbol);
            var items = _items.SetItem(symbol, mutation(shares));
            return new Holdings(items);
        }

        public IEnumerator<ShareCount> GetEnumerator() => _items.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}