using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Shared.Application.Messaging;
using Swashbuckle.AspNetCore.Annotations;

namespace StockTrader.Portfolios.Api.Endpoints
{
    [ApiController]
    public class ClosePortfolioEndpoint : ControllerBase
    {
        private readonly ICommandBus _bus;

        public ClosePortfolioEndpoint(ICommandBus bus) => _bus = bus;

        [HttpPut("api/portfolio/{id}/close")]
        [SwaggerOperation(
            Summary = "Close portfolio",
            Description = "Close an existing portfolio.",
            OperationId = nameof(ClosePortfolio),
            Tags = new[] { "Portfolios" })]
        public async Task<ActionResult> Call(string id)
        {
            await _bus.Send(new ClosePortfolio(id));
            return Accepted();
        }
    }
}
