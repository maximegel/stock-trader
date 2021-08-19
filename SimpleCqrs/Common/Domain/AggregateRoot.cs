namespace SimpleCqrs.Common.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
        where TId : Identifier
    {
        protected AggregateRoot(TId id) : base(id) { }
    }
}