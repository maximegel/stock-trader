namespace StockTrader.Shared.Domain
{
    public interface IAggregateRoot : IEntity
    {
    }

    public interface IAggregateRoot<out TId> :
        IAggregateRoot,
        IEntity<TId>
        where TId : IIdentifier
    {
    }
}
