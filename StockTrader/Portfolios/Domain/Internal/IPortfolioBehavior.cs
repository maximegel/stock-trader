namespace StockTrader.Portfolios.Domain.Internal
{
    internal interface IPortfolioBehavior
    {
        void Open();
        void DebitShares(ShareCount shares);
        void Close();
    }
}