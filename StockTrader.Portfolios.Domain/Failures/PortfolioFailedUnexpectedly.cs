using System;

namespace StockTrader.Portfolios.Domain.Failures
{
    public record PortfolioFailedUnexpectedly : PortfolioFailure
    {
        internal PortfolioFailedUnexpectedly(Exception e)
            : base("Portfolio failed unexpectedly.") { }
    }
}