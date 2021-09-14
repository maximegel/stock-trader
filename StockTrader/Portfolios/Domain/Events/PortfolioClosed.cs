using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain.Events
{
    public record PortfolioClosed
        : PortfolioEvent
    {
        internal override void ApplyTo(IPortfolioBehavior portfolio)
        {
            portfolio.Close();
        }
    }
}