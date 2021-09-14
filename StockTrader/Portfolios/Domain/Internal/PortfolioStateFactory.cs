using System;
using StockTrader.Portfolios.Domain.Internal.States;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal static class PortfolioStateFactory
    {
        public static IPortfolioState FromLabel(PortfolioStatus label) =>
            label switch
            {
                PortfolioStatus.Nil => new Nil(),
                PortfolioStatus.Opened => new Opened(),
                PortfolioStatus.Closed => new Closed(),
                _ => throw new ArgumentOutOfRangeException(nameof(label))
            };
    }
}