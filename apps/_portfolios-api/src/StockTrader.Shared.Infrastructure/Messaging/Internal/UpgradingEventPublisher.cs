using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StockTrader.Shared.Application.Messaging;

namespace StockTrader.Shared.Infrastructure.Messaging.Internal
{
    internal class UpgradingEventPublisher<TSource, TDestination> : EventPublisher<TSource>
        where TSource : class
        where TDestination : class
    {
        private readonly IEventUpgrader<TSource, TDestination> _upgrader;
        private readonly IEventPublisher<TDestination> _decorated;

        public UpgradingEventPublisher(
            IEventUpgrader<TSource, TDestination> upgrader,
            IEventPublisher<TDestination> decorated)
        {
            _upgrader = upgrader;
            _decorated = decorated;
        }

        protected override async Task Publish(
            IEnumerable<TSource> events,
            CancellationToken cancellationToken = default)
        {
            var upgradedEvents = _upgrader.Upgrade(events);
            await _decorated.Publish(upgradedEvents, cancellationToken);
        }
    }
}
