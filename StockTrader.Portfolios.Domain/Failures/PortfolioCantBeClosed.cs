namespace StockTrader.Portfolios.Domain.Failures
{
    public record PortfolioCantBeClosed : PortfolioFailure
    {
        public PortfolioCantBeClosed()
            : base("Portfolio cannot be closed.") { }
    }
}