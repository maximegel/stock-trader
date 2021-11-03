using System.Threading;
using System.Threading.Tasks;
using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Domain;

namespace StockTrader.Shared.Infrastructure.Persistence.Internal
{
    internal class EventPublishingRepository<TAggregate, TEvent> : IRepository<TAggregate>
        where TAggregate : IAggregateRoot, IEventSourced<TAggregate, TEvent>
        where TEvent : class, IDomainEventDescriptor
    {
        private readonly IEventPublisher<TEvent> _publisher;
        private readonly IRepository<TAggregate> _decorated;

        public EventPublishingRepository(
            IEventPublisher<TEvent> publisher,
            IRepository<TAggregate> decorated)
        {
            _publisher = publisher;
            _decorated = decorated;
        }

        public Task<TAggregate?> Find(IIdentifier id, CancellationToken cancellationToken = default) =>
            _decorated.Find(id, cancellationToken);

        public async Task Save(TAggregate aggregate, CancellationToken cancellationToken = default)
        {
            await _decorated.Save(aggregate, cancellationToken);

            await _publisher.Publish(aggregate.UncommittedEvents, cancellationToken);
            aggregate.MarkAsCommitted();
        }
    }
}
