namespace StockTrader.Shared.Domain
{
    public interface IEntity
    {
        Identifier GetId();
    }

    public interface IEntity<out TId> : IEntity
        where TId : Identifier
    {
        TId Id { get; }
    }
}