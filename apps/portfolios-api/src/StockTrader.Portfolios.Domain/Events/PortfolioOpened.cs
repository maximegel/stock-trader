using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain.Events
{
    public record PortfolioOpened(string Name)
        : PortfolioEvent
    {
        internal override PortfolioState ApplyTo(PortfolioState portfolio) =>
            portfolio.Open();
    }
}
