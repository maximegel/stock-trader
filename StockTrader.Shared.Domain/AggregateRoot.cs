namespace StockTrader.Shared.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>,
        IAggregateRoot<TId>
        where TId : IIdentifier
    {
        protected AggregateRoot(TId id) : base(id) { }
    }
}