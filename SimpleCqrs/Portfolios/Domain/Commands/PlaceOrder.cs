using System;
using System.Collections.Generic;
using SimpleCqrs.Portfolios.Domain.Events;
using SimpleCqrs.Portfolios.Domain.Failures;
using SimpleCqrs.Portfolios.Domain.Internal;
using SimpleCqrs.Portfolios.Domain.Payloads;

namespace SimpleCqrs.Portfolios.Domain.Commands
{
    public record PlaceOrder(string AggregateId, OrderDetails Details) :
        PortfolioCommand(AggregateId)
    {
        internal override IEnumerable<PortfolioEvent> ExecuteOn(IPortfolioModel portfolio)
        {
            var symbol = new Symbol(Details.Symbol);
            var shares = new ShareCount(Details.Shares, symbol);
            var heldShares = portfolio.Holdings.CountOf(symbol);

            switch (Details.TradeType)
            {
                case TradeType.Sell:
                    if (portfolio.Holdings.CanDebit(shares))
                        // TODO: Make idempotent.
                        yield return new SharesDebited(
                            symbol.ToString(),
                            shares.ToInt());
                    else
                        yield return new InsufficientShares(shares, heldShares);
                    break;
                case TradeType.Buy:
                    break;
                default:
                    throw new InvalidOperationException();
            }

            // TODO: Make idempotent.
            yield return new OrderPlaced(OrderId.Generate(), Details);
        }
    }
}