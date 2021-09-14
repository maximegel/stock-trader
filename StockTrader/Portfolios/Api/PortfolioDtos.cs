using StockTrader.Portfolios.Domain.Payloads;

namespace StockTrader.Portfolios.Api
{
    public record PlaceOrderDto(OrderDetails Details);

    public record OpenPortfolioDto(string Name);
}