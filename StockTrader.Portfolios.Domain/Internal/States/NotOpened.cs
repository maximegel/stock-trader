namespace StockTrader.Portfolios.Domain.Internal.States
{
    internal record NotOpened : PortfolioState<NotOpened>
    {
        public override PortfolioState Open() => new Opened();
    }
}
