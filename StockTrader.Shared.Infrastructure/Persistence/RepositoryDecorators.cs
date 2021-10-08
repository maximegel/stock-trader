using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Domain;
using StockTrader.Shared.Infrastructure.Persistence.Internal;

namespace StockTrader.Shared.Infrastructure.Persistence
{
    public static class RepositoryDecorators
    {
        public static IRepository<TAggregate> UseImmediateEventPublisher<TAggregate>(
            this IRepository<TAggregate> repository,
            IEventPublisher eventPublisher)
            where TAggregate : IAggregateRoot, IEventSourced
        {
            return new EventPublishingRepository<TAggregate>(eventPublisher, repository);
        }
    }
}
