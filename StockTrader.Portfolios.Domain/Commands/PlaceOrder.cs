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
                return Yield(new PortfolioCantBeClosed());

            return Trade.OfType(Details.TradeType)
                .WhenBuy(() => Yield(new OrderPlaced(OrderId.Generate(), Details)))
                .WhenSell(() =>
                {
                    var symbol = new Symbol(Details.Symbol);
                    var sellingShares = new ShareCount(Details.Shares, symbol);
                    var heldShares = portfolio.Holdings.CountOf(symbol);

                    if (heldShares < sellingShares)
                        return Yield(new InsufficientShares(heldShares, sellingShares));
                    
                    var remainingShares = heldShares.Debit(sellingShares);
                    return Yield(
                        new SharesDebited(sellingShares, remainingShares, symbol),
                        new OrderPlaced(OrderId.Generate(), Details));
                });
        }
    }
}