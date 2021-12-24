using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockTrader.Portfolios.Application.Queries;
using StockTrader.Portfolios.Projection.PortfolioDetails;
using StockTrader.Shared.Application.Messaging;
using Swashbuckle.AspNetCore.Annotations;

namespace StockTrader.Portfolios.Api.Queries
{
    [ApiController]
    public class GetPortfolioDetailEndpoint : ControllerBase
    {
        private readonly IQueryBus _bus;

        public GetPortfolioDetailEndpoint(IQueryBus bus) =>
            _bus = bus;

        [HttpGet("api/portfolio/{id}")]
        [SwaggerOperation(
            Summary = "Get portfolio detail",
            Description = "Get the name and a high-level view of a portfolio's performance.",
            OperationId = "GetPortfolioDetail",
            Tags = new[] { "Portfolios" })]
        public async Task<ActionResult<PortfolioDetailView>> Invoke(string id) =>
            await _bus.Send(new GetPortfolioDetail(id));
    }
}
