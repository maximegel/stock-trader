using System;
using StockTrader.Shared.Application.Messaging;

namespace StockTrader.Shared.Infrastructure.Messaging.Internal
{
    internal class InlineEventUpgrader<TSource, TDestination> :
        IEventUpgrader<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {
        private readonly Func<TSource, TDestination> _func;

        public InlineEventUpgrader(Func<TSource, TDestination> func) =>
            _func = func;

        public TDestination Upgrade(TSource sourceEvent) =>
            _func(sourceEvent);
    }
}
