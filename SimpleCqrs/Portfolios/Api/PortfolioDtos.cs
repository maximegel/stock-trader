using SimpleCqrs.Portfolios.Domain.Payloads;

namespace SimpleCqrs.Portfolios.Api
{
    public record PlaceOrderDto(OrderDetails Details);

    public record OpenPortfolioDto(string Name);
}