using System;
using SimpleCqrs.Common.Domain;
using SimpleCqrs.Portfolios.Domain.Internal;

namespace SimpleCqrs.Portfolios.Domain
{
    public abstract record PortfolioEvent : IDomainEvent
    {
        internal abstract void ApplyTo(IPortfolioBehavior portfolio);
    }
}