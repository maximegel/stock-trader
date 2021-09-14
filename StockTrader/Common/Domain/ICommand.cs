namespace StockTrader.Common.Domain
{
    public interface ICommand
    {
        string AggregateId { get; }
    }
}