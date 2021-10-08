using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Portfolios.Domain.Payloads;
using StockTrader.Shared.Application.Messaging;
using Swashbuckle.AspNetCore.Annotations;

namespace StockTrader.Portfolios.Api.Endpoints
{
    [ApiController]
    public class PlaceOrderEndpoint : ControllerBase
    {
        private readonly ICommandBus _bus;

        public PlaceOrderEndpoint(ICommandBus bus) => _bus = bus;

        [HttpPut("api/portfolio/{id}/place-order")]
        [SwaggerOperation(
            Summary = "Place order",
            Description = "Sell or buy shares of a portfolio.",
            OperationId = "PlaceOrder",
            Tags = new[] { "Portfolios" })]
        public async Task<ActionResult> Call(string id, PlaceOrderDto dto)
        {
            await _bus.Send(new PlaceOrder(id, dto.Details));
            return Accepted();
        }
    }

    public record PlaceOrderDto(OrderDetails Details);
}
