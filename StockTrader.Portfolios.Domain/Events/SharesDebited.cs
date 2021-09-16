using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain.Events
{
    public record SharesDebited(string Symbol, int Shares) 
        : PortfolioEvent
    {
        internal override void ApplyTo(IPortfolioBehavior portfolio)
        {
            var symbol = new Symbol(Symbol);
            var shares = new ShareCount(Shares, symbol);
            portfolio.DebitShares(shares);
        }
    }
}