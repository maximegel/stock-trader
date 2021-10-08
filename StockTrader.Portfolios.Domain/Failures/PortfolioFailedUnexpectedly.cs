namespace StockTrader.Portfolios.Domain.Failures
{
    public record PortfolioFailedUnexpectedly : PortfolioFailure
    {
        internal PortfolioFailedUnexpectedly()
            : base("Portfolio failed unexpectedly.")
        {
        }
    }
}
