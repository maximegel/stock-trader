using System.Collections.Generic;
using NodaTime;
using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public abstract record PortfolioCommand : ICommand
    {
        protected PortfolioCommand(string aggregateId) =>
            AggregateId = aggregateId;

        public string AggregateId { get; }

        public Instant Timestamp { get; init; } = SystemClock.Instance.GetCurrentInstant();

        internal abstract IEnumerable<PortfolioEvent> ExecuteOn(PortfolioModel portfolio);

        protected static IEnumerable<PortfolioEvent> Yield(params PortfolioEvent[] events) =>
            events;
    }
}
