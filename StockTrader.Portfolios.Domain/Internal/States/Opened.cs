namespace StockTrader.Portfolios.Domain.Internal.States
{
    internal record Opened : PortfolioState<Opened>
    {
        public override PortfolioState SetShares(ShareCount shares)
        {
            return this with { Holdings = Holdings.SetCount(shares) };
        }

        public override PortfolioState Close() => new Closed();
    }
}
