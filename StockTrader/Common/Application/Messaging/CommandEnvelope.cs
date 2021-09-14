using MediatR;
using StockTrader.Common.Domain;

namespace StockTrader.Common.Application.Messaging
{
    public static class CommandEnvelope
    {
        public static CommandEnvelope<TCommand> For<TCommand>(TCommand command)
            where TCommand : ICommand =>
            new(command);
    }

    public record CommandEnvelope<TCommand> : IRequest
        where TCommand : ICommand
    {
        private readonly TCommand _command;

        internal CommandEnvelope(TCommand command) => _command = command;

        public TCommand AsCommand() => _command;

        public static implicit operator TCommand(CommandEnvelope<TCommand> envelope) => envelope.AsCommand();
    }
}