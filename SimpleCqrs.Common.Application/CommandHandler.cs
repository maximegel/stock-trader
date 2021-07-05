using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application
{
    public abstract class CommandHandler<TCommand, TAggregate> : AsyncRequestHandler<CommandEnvelope<TCommand>>
        where TCommand : ICommand<TAggregate>
        where TAggregate : IAggregateRoot<TAggregate>
    {
        protected override Task Handle(CommandEnvelope<TCommand> envelope, CancellationToken cancellationToken) =>
            Handle(envelope.Data, cancellationToken);

        protected abstract Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}