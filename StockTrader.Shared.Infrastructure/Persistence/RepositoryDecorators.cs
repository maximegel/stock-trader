using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Domain;
using StockTrader.Shared.Infrastructure.Persistence.Internal;

namespace StockTrader.Shared.Infrastructure.Persistence
{
    public static class RepositoryDecorators
    {
        public static IRepository<TAggregate> UseImmediateEventPublisher<TAggregate, TEvent>(
            this IRepository<TAggregate> repository,
            IEventPublisher<TEvent> publisher)
            where TAggregate : IAggregateRoot, IEventSourced<TAggregate, TEvent>
            where TEvent : class, IDomainEventDescriptor
        {
            return new EventPublishingRepository<TAggregate, TEvent>(publisher, repository);
        }
    }
}
