using System.Collections.Generic;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain.Commands
{
    public record OpenPortfolio(string Name) : PortfolioCommand(PortfolioId.Generate())
    {
        internal override IEnumerable<PortfolioEvent> ExecuteOn(IPortfolioModel portfolio)
        {
            yield return new PortfolioOpened(Name);
        }
    }
}