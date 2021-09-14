using SimpleCqrs.Common.Domain;
using SimpleCqrs.Portfolios.Domain.Internal;

namespace SimpleCqrs.Portfolios.Domain
{
    public abstract record PortfolioFailure(string Message) : PortfolioEvent,
        IDomainFailure
    {
        public static implicit operator DomainException(PortfolioFailure failure) =>
            new(failure.Message);

        internal override void ApplyTo(IPortfolioBehavior portfolio) { }
    }
}