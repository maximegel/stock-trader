using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleCqrs.Common.Application.Messaging;
using SimpleCqrs.Portfolios.Domain.Commands;

namespace SimpleCqrs.Portfolios.Api
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly ICommandBus _bus;

        public PortfolioController(ICommandBus bus) => _bus = bus;

        /// <summary>
        ///     Open a new portfolio.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("open")]
        public async Task<ActionResult> Open(OpenPortfolioDto dto)
        {
            await _bus.Send(new OpenPortfolio(dto.Name));
            return Accepted();
        }

        /// <summary>
        ///     Close an existing portfolio.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/close")]
        public async Task<ActionResult> Close(string id)
        {
            await _bus.Send(new ClosePortfolio(id));
            return Accepted();
        }

        /// <summary>
        ///     Sell or buy shares of a portfolio.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}/place-order")]
        public async Task<ActionResult> PlaceOrder(string id, PlaceOrderDto dto)
        {
            await _bus.Send(new PlaceOrder(id, dto.Details));
            return Accepted();
        }
    }
}