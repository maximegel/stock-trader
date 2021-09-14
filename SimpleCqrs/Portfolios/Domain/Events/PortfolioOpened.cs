using SimpleCqrs.Portfolios.Domain.Internal;

namespace SimpleCqrs.Portfolios.Domain.Events
{
    public record PortfolioOpened(string Name)
        : PortfolioEvent
    {
        internal override void ApplyTo(IPortfolioBehavior portfolio)
        {
            portfolio.Open();
        }
    }
}