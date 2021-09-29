namespace StockTrader.Portfolios.Domain.Payloads
{
    public record OrderDetails(
        TradeType TradeType,
        int Shares,
        string Symbol,
        OrderType OrderType)
    {
        public decimal? PriceLimit { get; init; }
    }
}