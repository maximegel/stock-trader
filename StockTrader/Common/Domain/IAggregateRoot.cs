namespace StockTrader.Common.Domain
{
    public interface IAggregateRoot : IEntity { }

    public interface IAggregateRoot<out TId> : 
        IAggregateRoot, 
        IEntity<TId> 
        where TId : Identifier { }
}