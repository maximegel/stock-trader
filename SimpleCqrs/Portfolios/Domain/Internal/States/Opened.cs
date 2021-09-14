using System;

namespace SimpleCqrs.Portfolios.Domain.Internal.States
{
    internal class Opened : IPortfolioState
    {
        public PortfolioStatus Status => PortfolioStatus.Opened;
        
        public IPortfolioState Open() => this;

        public IPortfolioState DebitShares(Action action) => this;

        public IPortfolioState Close() => 
            new Closed();
    }
}