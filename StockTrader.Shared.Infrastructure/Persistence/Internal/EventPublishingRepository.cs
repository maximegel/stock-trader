using System.Threading;
using System.Threading.Tasks;
using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Domain;

namespace StockTrader.Shared.Infrastructure.Persistence.Internal
{
    internal class EventPublishingRepository<TAggregate> : IRepository<TAggregate>
        where TAggregate : IAggregateRoot, IEventSourced
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<TAggregate> _decorated;

        public EventPublishingRepository(IEventPublisher eventPublisher, IRepository<TAggregate> decorated)
        {
            _eventPublisher = eventPublisher;
            _decorated = decorated;
        }

        public Task<TAggregate?> Find(IIdentifier id, CancellationToken cancellationToken = default) =>
            _decorated.Find(id, cancellationToken);

        public async Task Save(TAggregate aggregate, CancellationToken cancellationToken = default)
        {
            await _decorated.Save(aggregate, cancellationToken);

            var integrationEvents = aggregate.UncommittedEvents.ToIntegrationEvents();
            await _eventPublisher.Publish(integrationEvents, cancellationToken);
            aggregate.ClearUncommittedEvents();
        }
    }
}
