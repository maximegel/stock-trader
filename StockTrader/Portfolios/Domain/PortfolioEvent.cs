using System;
using StockTrader.Common.Domain;
using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain
{
    public abstract record PortfolioEvent : IDomainEvent
    {
        internal abstract void ApplyTo(IPortfolioBehavior portfolio);
    }
}