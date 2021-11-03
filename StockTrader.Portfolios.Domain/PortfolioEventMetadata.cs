using NodaTime;

namespace StockTrader.Portfolios.Domain
{
    public record PortfolioEventMetadata(
        Instant Timestamp);
}
