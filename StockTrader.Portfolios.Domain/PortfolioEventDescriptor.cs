using NodaTime;
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
            Metadata)
    {
        public PortfolioEventDescriptor(PortfolioId aggregateId, PortfolioEvent data)
            : this(
                aggregateId.ToString(),
                data,
                new PortfolioEventMetadata(SystemClock.Instance.GetCurrentInstant()))
        {
        }
    }
}
