using System;

namespace StockTrader.Portfolios.Domain.Internal.States
{
    internal class Closed : IPortfolioState
    {
        public PortfolioStatus Status => PortfolioStatus.Closed;

        public IPortfolioState Open() => 
            throw new InvalidOperationException();

        public IPortfolioState DebitShares(Action action) => 
            throw new InvalidOperationException();

        public IPortfolioState Close() => this;
    }
}