namespace StockTrader.Shared.Domain
{
    public abstract record DomainEventDescriptor<TDomainEvent>(
        string AggregateId,
        TDomainEvent Data) :
        IDomainEventDescriptor<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        public TDomainEvent AsDomainEvent() => Data;

        IDomainEvent IDomainEventDescriptor.AsDomainEvent() => Data;
    }

    public abstract record DomainEventDescriptor<TDomainEvent, TMetadata>(
        string AggregateId,
        TDomainEvent Data,
        TMetadata Metadata) :
        DomainEventDescriptor<TDomainEvent>(AggregateId, Data)
        where TDomainEvent : IDomainEvent;
}
