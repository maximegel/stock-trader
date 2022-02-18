namespace StockTrader.Shared.Domain
{
    public abstract class AggregateRoot<TSelf, TId> : Entity<TSelf, TId>,
        IAggregateRoot<TId>
        where TSelf : IAggregateRoot<TId>
        where TId : IIdentifier
    {
        protected AggregateRoot(TId id)
            : base(id)
        {
        }
    }
}
