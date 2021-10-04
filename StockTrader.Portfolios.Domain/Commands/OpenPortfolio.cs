using System.Collections.Generic;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Failures;
using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Portfolios.Domain.Internal.States;

namespace StockTrader.Portfolios.Domain.Commands
{
    public record OpenPortfolio(string Name) : PortfolioCommand(PortfolioId.Generate())
    {
        internal override IEnumerable<PortfolioEvent> ExecuteOn(PortfolioModel portfolio)
        {
            if (portfolio.Status.Is<NotOpened>())
                yield return new PortfolioOpened(Name);
            else
                yield return new PortfolioAlreadyOpened();
        }
    }
}