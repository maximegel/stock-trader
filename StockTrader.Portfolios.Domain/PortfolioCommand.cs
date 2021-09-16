using System.Collections.Generic;
using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public abstract record PortfolioCommand : ICommand
    {
        protected PortfolioCommand(string aggregateId) =>
            AggregateId = aggregateId;

        public string AggregateId { get; }
        
        internal abstract IEnumerable<PortfolioEvent> ExecuteOn(IPortfolioModel portfolio);
    }
}