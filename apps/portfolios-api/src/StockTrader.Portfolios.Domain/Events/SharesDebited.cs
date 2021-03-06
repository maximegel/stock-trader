using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain.Events
{
    public record SharesDebited(int DebitedShares, int RemainingShares, string Symbol)
        : PortfolioEvent
    {
        internal override PortfolioState ApplyTo(PortfolioState portfolio)
        {
            var symbol = new Symbol(Symbol);
            var remaining = new ShareCount(RemainingShares, symbol);
            return portfolio.SetShares(remaining);
        }
    }
}
