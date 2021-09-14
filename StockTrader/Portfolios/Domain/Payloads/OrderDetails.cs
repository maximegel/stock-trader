namespace StockTrader.Portfolios.Domain.Payloads
{
    public record OrderDetails(
        string Symbol,
        int Shares,
        TradeType TradeType,
        OrderType OrderType)
    {
        public decimal? PriceLimit { get; init; }
    }
}