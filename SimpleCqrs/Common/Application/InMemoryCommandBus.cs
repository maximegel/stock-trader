using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application
{
    public class InMemoryCommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public InMemoryCommandBus(IMediator mediator) => _mediator = mediator;

        public Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command =>
            _mediator.Send(CommandEnvelope.For(command), cancellationToken);
    }
}