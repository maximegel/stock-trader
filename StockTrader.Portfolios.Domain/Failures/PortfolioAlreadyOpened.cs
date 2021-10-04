namespace StockTrader.Portfolios.Domain.Failures
{
    public record PortfolioAlreadyOpened : PortfolioFailure
    {
        public PortfolioAlreadyOpened()
            : base("Portfolio already opened.") { }
    }
}