using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain.Failures
{
    public record InsufficientShares : PortfolioFailure
    {
        public InsufficientShares(int heldShares, int requiredShares, string symbol)
            : this(
                new ShareCount(heldShares, new Symbol(symbol)),
                new ShareCount(requiredShares, new Symbol(symbol)))
        {
        }

        internal InsufficientShares(ShareCount held, ShareCount required)
            : base($"Insufficient shares, {held} required, {required} held.")
        {
        }
    }
}
