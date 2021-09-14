using System.Collections.Generic;
using SimpleCqrs.Portfolios.Domain.Events;
using SimpleCqrs.Portfolios.Domain.Internal;

namespace SimpleCqrs.Portfolios.Domain.Commands
{
    public record OpenPortfolio(string Name) : PortfolioCommand(PortfolioId.Generate())
    {
        internal override IEnumerable<PortfolioEvent> ExecuteOn(IPortfolioModel portfolio)
        {
            yield return new PortfolioOpened(Name);
        }
    }
}