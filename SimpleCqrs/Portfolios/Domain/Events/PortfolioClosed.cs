using SimpleCqrs.Portfolios.Domain.Internal;

namespace SimpleCqrs.Portfolios.Domain.Events
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