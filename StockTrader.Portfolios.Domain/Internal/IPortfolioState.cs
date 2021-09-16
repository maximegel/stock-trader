using System;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal interface IPortfolioState
    {
        PortfolioStatus Status { get; }
        
        IPortfolioState Open();
        IPortfolioState DebitShares(Action action);
        IPortfolioState Close();
    }
}