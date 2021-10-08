using System.Collections.Generic;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Portfolios.Domain.Internal.States;

namespace StockTrader.Portfolios.Domain.Commands
{
    public record ClosePortfolio(string AggregateId)
        : PortfolioCommand(AggregateId)
    {
        internal override IEnumerable<PortfolioEvent> ExecuteOn(PortfolioModel portfolio)
        {
            if (portfolio.Status.Is<Closed>())
            {
                yield break;
            }

            yield return new PortfolioClosed();
        }
    }
}
