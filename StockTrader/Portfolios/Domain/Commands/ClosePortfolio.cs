using System.Collections.Generic;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Domain.Commands
{
    public record ClosePortfolio(string AggregateId) : PortfolioCommand(AggregateId)
    {
        internal override IEnumerable<PortfolioEvent> ExecuteOn(IPortfolioModel portfolio)
        {
            yield return new PortfolioClosed();
        }
    }
}