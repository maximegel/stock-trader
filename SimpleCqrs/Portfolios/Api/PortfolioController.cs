using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleCqrs.Common.Application;
using SimpleCqrs.Portfolios.Application;

namespace SimpleCqrs.Portfolios.Api
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly ICommandBus _bus;

        public PortfolioController(ICommandBus bus) => _bus = bus;

        [HttpPost]
        public async Task<ActionResult> Open(OpenPortfolioDto dto)
        {
            await _bus.Send(new OpenPortfolio(dto.Name));
            return Accepted();
        }

        [HttpPut("{id}/close")]
        public async Task<ActionResult> Close(string id)
        {
            await _bus.Send(new ClosePortfolio());
            return Accepted();
        }

        [HttpPut("{id}/place-order")]
        public async Task<ActionResult> PlaceOrder(string id, PlaceOrderDto dto)
        {
            await _bus.Send(new PlaceOrder());
            return Accepted();
        }
    }

    public record OpenPortfolioDto(string Name);

    public record PlaceOrderDto;
}