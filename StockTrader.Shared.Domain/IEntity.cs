namespace StockTrader.Shared.Domain
{
    public interface IEntity
    {
        IIdentifier Id { get; }
    }

    public interface IEntity<out TId> : IEntity
        where TId : IIdentifier
    {
        new TId Id { get; }
    }
}