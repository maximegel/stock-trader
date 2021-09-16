using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public abstract record PortfolioFailure(string Message) : PortfolioEvent,
        IDomainFailure
    {
        public static implicit operator DomainException(PortfolioFailure failure) =>
            new(failure.Message);

        internal override void ApplyTo(IPortfolioBehavior portfolio) { }
    }
}