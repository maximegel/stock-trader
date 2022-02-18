using MediatR;

namespace StockTrader.Shared.Application.Messaging
{
    public interface IIntegrationEvent : INotification
    {
        string AggregateId { get; }
    }
}
