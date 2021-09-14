using System;

namespace SimpleCqrs.Portfolios.Domain.Internal.States
{
    internal class Nil : IPortfolioState
    {
        public PortfolioStatus Status => PortfolioStatus.Nil;
        
        public IPortfolioState Open() =>
            new Opened();

        public IPortfolioState DebitShares(Action action) => 
            throw new InvalidOperationException();

        public IPortfolioState Close() => 
            throw new InvalidOperationException();
    }
}