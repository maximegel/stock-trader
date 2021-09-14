using System.Collections.Generic;
using SimpleCqrs.Portfolios.Domain.Events;
using SimpleCqrs.Portfolios.Domain.Internal;

namespace SimpleCqrs.Portfolios.Domain.Commands
{
    public record ClosePortfolio(string AggregateId) : PortfolioCommand(AggregateId)
    {
        internal override IEnumerable<PortfolioEvent> ExecuteOn(IPortfolioModel portfolio)
        {
            yield return new PortfolioClosed();
        }
    }
}