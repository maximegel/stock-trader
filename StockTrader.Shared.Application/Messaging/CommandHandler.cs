using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StockTrader.Shared.Domain;

namespace StockTrader.Shared.Application.Messaging
{
    public abstract class CommandHandler<TCommand> :
        AsyncRequestHandler<CommandEnvelope<TCommand>>
        where TCommand : ICommand
    {
        protected override Task Handle(
            CommandEnvelope<TCommand> envelope,
            CancellationToken cancellationToken)
        {
            return Handle(envelope.Payload, cancellationToken);
        }

        protected abstract Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}
