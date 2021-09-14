using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application.Messaging
{
    public abstract class CommandHandler<TCommand> : AsyncRequestHandler<CommandEnvelope<TCommand>>
        where TCommand : ICommand
    {
        protected override Task Handle(CommandEnvelope<TCommand> envelope, CancellationToken cancellationToken) =>
            Handle(envelope.AsCommand(), cancellationToken);

        protected abstract Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}