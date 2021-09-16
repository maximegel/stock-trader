using StockTrader.Portfolios.Domain.Payloads;

namespace StockTrader.Portfolios.Api.Controllers
{
    public record PlaceOrderDto(OrderDetails Details);

    public record OpenPortfolioDto(string Name);
}