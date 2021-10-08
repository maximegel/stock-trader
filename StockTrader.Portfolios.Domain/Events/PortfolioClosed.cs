using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain.Events
{
    public record PortfolioClosed
        : PortfolioEvent
    {
        internal override PortfolioState ApplyTo(PortfolioState portfolio) =>
            portfolio.Close();
    }
}
