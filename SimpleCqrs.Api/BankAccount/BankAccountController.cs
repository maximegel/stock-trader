using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleCqrs.Common.Application;
using SimpleCqrs.Domain.Commands;

namespace SimpleCqrs.Api.BankAccount
{
    [ApiController]
    [Route("api/bank-account")]
    public class BankAccountController : ControllerBase
    {
        private readonly ICommandBus _bus;

        public BankAccountController(ICommandBus bus) => _bus = bus;

        [HttpPost]
        public async Task<ActionResult> Open(OpenDto dto)
        {
            await _bus.Send(new OpenCommand());
            return Accepted();
        }

        [HttpPut("{id}/freeze")]
        public async Task<ActionResult> Freeze(string id)
        {
            await _bus.Send(new FreezeCommand(id));
            return Accepted();
        }

        [Route("{id}/withdraw")]
        public async Task<ActionResult> Withdraw(string id, WithdrawDto dto)
        {
            await _bus.Send(new WithdrawCommand(id, dto.Amount));
            return Accepted();
        }
    }
}