using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Portfolios.Domain.Payloads;

namespace StockTrader.Portfolios.Domain.Events
{
    public record OrderPlaced(string OrderId, OrderDetails Details)
        : PortfolioEvent
    {
        internal override PortfolioState ApplyTo(PortfolioState portfolio) =>
            portfolio;
    }
}
