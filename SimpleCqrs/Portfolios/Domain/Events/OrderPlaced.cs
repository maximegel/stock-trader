using SimpleCqrs.Portfolios.Domain.Internal;
using SimpleCqrs.Portfolios.Domain.Payloads;

namespace SimpleCqrs.Portfolios.Domain.Events
{
    public record OrderPlaced(string OrderId, OrderDetails Details)
        : PortfolioEvent
    {
        internal override void ApplyTo(IPortfolioBehavior portfolio)
        {
            var symbol = new Symbol(Details.Symbol);
            var shares = new ShareCount(Details.Shares, symbol);
            portfolio.DebitShares(shares);
        }
    }
}