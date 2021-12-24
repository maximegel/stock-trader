namespace StockTrader.Shared.Application.Messaging
{
    public abstract record Query<TResult> : IQuery<TResult>;
}
