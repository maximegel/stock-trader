using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public abstract record PortfolioEvent : IDomainEvent
    {
        internal abstract PortfolioState ApplyTo(PortfolioState portfolio);

        internal PortfolioEventDescriptor ToDescriptor(
            Portfolio portfolio, PortfolioCommand command)
        {
            return new PortfolioEventDescriptor(
                portfolio.Id,
                this,
                new PortfolioEventMetadata(command.Timestamp));
        }
    }
}
