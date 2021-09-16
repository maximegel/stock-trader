using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain.Failures
{
    public record InsufficientShares : PortfolioFailure
    {
        internal InsufficientShares(ShareCount required, ShareCount held)
            : base($"Insufficient shares, {required} required, {held} held.")
        {
        }
    }
}