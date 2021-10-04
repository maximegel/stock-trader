using System;
using System.Collections.Generic;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Failures;
using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Portfolios.Domain.Internal.States;
using StockTrader.Portfolios.Domain.Payloads;

namespace StockTrader.Portfolios.Domain.Commands
{
    public record PlaceOrder(string AggregateId, OrderDetails Details) :
        PortfolioCommand(AggregateId)
    {
        internal override IEnumerable<PortfolioEvent> ExecuteOn(PortfolioModel portfolio)
        {
            if (portfolio.Status.Is<Closed>())
            {
                yield return new PortfolioCantBeClosed();
                yield break;
            }
            
            var symbol = new Symbol(Details.Symbol);
            var shares = new ShareCount(Details.Shares, symbol);
            var heldShares = portfolio.Holdings.CountOf(symbol);

            switch (Details.TradeType)
            {
                case TradeType.Sell:
                    if (portfolio.Holdings.CanDebit(shares))
                    {
                        var remainingShares = heldShares.Debit(shares);
                        yield return new SharesDebited(shares, remainingShares, symbol);
                    }
                        
                    else
                    {
                        yield return new InsufficientShares(heldShares, shares);
                        yield break;
                    }
                    break;
                case TradeType.Buy:
                    break;
                default:
                    throw new InvalidOperationException();
            }

            // TODO: Make order id predictable.
            yield return new OrderPlaced(OrderId.Generate(), Details);
        }
    }
}