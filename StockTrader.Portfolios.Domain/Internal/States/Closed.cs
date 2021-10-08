namespace StockTrader.Portfolios.Domain.Internal.States
{
    internal record Closed : PortfolioState<Closed>
    {
        public override PortfolioState Close() => this;
    }
}
