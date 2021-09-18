using MediatR;
using StockTrader.Shared.Domain;

namespace StockTrader.Shared.Application.Messaging
{
    public record IntegrationEvent(string AggregateId, IDomainEvent Payload) :
        INotification
    {
        public static IntegrationEvent Of(
            IAggregateRoot aggregate, IDomainEvent domainEvent)
        {
            return new IntegrationEvent(aggregate.Id.ToString(), domainEvent);
        }

        public IDomainEvent AsDomainEvent() => Payload;
    }
}