namespace SimpleCqrs.Common.Domain
{
    public interface IDomainEvent { }

    public interface IDomainEvent<TAggregate> where TAggregate : IAggregateRoot
    {
        TAggregate Apply(TAggregate aggregate);
    }
}