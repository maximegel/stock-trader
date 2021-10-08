using MediatR;
using StockTrader.Shared.Domain;

namespace StockTrader.Shared.Application.Messaging
{
    public static class CommandEnvelope
    {
        public static CommandEnvelope<TCommand> Of<TCommand>(TCommand command)
            where TCommand : ICommand =>
            new(command);
    }

    public record CommandEnvelope<TCommand>(TCommand Payload) : IRequest
        where TCommand : ICommand
    {
        public TCommand AsCommand() => Payload;
    }
}
