using System.Collections.Generic;
using SimpleCqrs.Common.Domain;
using SimpleCqrs.Portfolios.Domain.Internal;

namespace SimpleCqrs.Portfolios.Domain
{
    public abstract record PortfolioCommand : ICommand
    {
        protected PortfolioCommand(string aggregateId) =>
            AggregateId = aggregateId;

        public string AggregateId { get; }
        
        internal abstract IEnumerable<PortfolioEvent> ExecuteOn(IPortfolioModel portfolio);
    }
}