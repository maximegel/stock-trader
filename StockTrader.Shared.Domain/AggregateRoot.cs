namespace StockTrader.Shared.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>,
        IAggregateRoot<TId>
        where TId : Identifier
    {
        protected AggregateRoot(TId id) : base(id) { }
    }
}