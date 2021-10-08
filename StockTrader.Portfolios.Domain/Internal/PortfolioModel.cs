namespace StockTrader.Portfolios.Domain.Internal
{
    internal class PortfolioModel
    {
        public PortfolioModel(PortfolioState state)
        {
            Holdings = state.Holdings;
            Status = state.Status;
        }

        public Holdings Holdings { get; }

        public PortfolioStatus Status { get; }
    }
}
