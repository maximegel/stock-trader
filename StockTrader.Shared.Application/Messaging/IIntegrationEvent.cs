namespace StockTrader.Shared.Application.Messaging
{
    public interface IIntegrationEvent
    {
        string AggregateId { get; }
    }
}
