using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal class Holdings : IEnumerable<ShareCount>
    {
        private readonly ImmutableDictionary<Symbol, ShareCount> _items;

        public Holdings(IEnumerable<ShareCount> items)
            : this(items.ToImmutableDictionary(item => item.Symbol))
        {
        }

        private Holdings(ImmutableDictionary<Symbol, ShareCount> items) =>
            _items = items;

        public static Holdings Empty { get; } =
            new(ImmutableDictionary<Symbol, ShareCount>.Empty);

        public ShareCount CountOf(Symbol symbol)
        {
            return HoldSharesOf(symbol)
                ? _items[symbol]
                : ShareCount.Zero(symbol);
        }

        public Holdings SetCount(ShareCount count)
        {
            var items = _items.SetItem(count.Symbol, count);
            return new Holdings(items);
        }

        public IEnumerator<ShareCount> GetEnumerator() =>
            _items.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private bool HoldSharesOf(Symbol symbol) =>
            _items.ContainsKey(symbol);
    }
}
