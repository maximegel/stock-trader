using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application
{
    public class InMemoryBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator) => _mediator = mediator;

        public Task Send(ICommand command, CancellationToken cancellationToken = default) =>
            _mediator.Send(command, cancellationToken);
    }
}