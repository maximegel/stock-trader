namespace StockTrader.Shared.Domain
{
    public interface ICommand
    {
        string AggregateId { get; }
    }
}
