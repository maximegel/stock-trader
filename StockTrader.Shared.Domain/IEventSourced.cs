namespace StockTrader.Shared.Domain
{
    public interface IEventSourced
    {
        IEventSource UncommittedEvents { get; }

        IEventSourced ClearUncommittedEvents();
    }
}