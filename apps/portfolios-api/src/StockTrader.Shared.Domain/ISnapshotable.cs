namespace StockTrader.Shared.Domain
{
    public interface ISnapshotable<out TEntity, TSnapshot>
        where TEntity : IEntity
    {
        TEntity RestoreSnapshot(TSnapshot snapshot);

        TSnapshot TakeSnapshot();
    }
}
