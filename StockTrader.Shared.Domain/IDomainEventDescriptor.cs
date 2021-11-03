namespace StockTrader.Shared.Domain
{
    public interface IDomainEventDescriptor
    {
        string AggregateId { get; }

        IDomainEvent AsDomainEvent();
    }

    public interface IDomainEventDescriptor<out TDomainEvent> :
        IDomainEventDescriptor
        where TDomainEvent : IDomainEvent
    {
        new TDomainEvent AsDomainEvent();
    }
}
