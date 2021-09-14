using System.Collections.Generic;
using StockTrader.Common.Domain;
using StockTrader.Portfolios.Domain.Internal;

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