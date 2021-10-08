using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Shared.Application.Messaging;
using Swashbuckle.AspNetCore.Annotations;

namespace StockTrader.Portfolios.Api.Endpoints
{
    [ApiController]
    public class OpenPortfolioEndpoint : ControllerBase
    {
        private readonly ICommandBus _bus;

        public OpenPortfolioEndpoint(ICommandBus bus) => _bus = bus;

        [HttpPost("api/portfolio/open")]
        [SwaggerOperation(
            Summary = "Open portfolio",
            Description = "Open a new portfolio.",
            OperationId = "OpenPortfolio",
            Tags = new[] { "Portfolios" })]
        public async Task<ActionResult> Call(OpenPortfolioDto dto)
        {
            await _bus.Send(new OpenPortfolio(dto.Name));
            return Accepted();
        }
    }

    public record OpenPortfolioDto(string Name);
}
