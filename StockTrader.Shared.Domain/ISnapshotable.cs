namespace StockTrader.Shared.Domain
{
    public interface ISnapshotable<out TSelf, TSnapshot>
        where TSelf : ISnapshotable<TSelf, TSnapshot>
    {
        TSelf RestoreSnapshot(TSnapshot snapshot);

        TSnapshot TakeSnapshot();
    }
}
