using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public record PortfolioEventDescriptor(
        string AggregateId,
        PortfolioEvent Data,
        PortfolioEventMetadata Metadata) :
        DomainEventDescriptor<PortfolioEvent, PortfolioEventMetadata>(
            AggregateId,
            Data,
            Metadata);
}
