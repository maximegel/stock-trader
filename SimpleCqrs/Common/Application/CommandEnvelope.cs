﻿using MediatR;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application
{
    public static class CommandEnvelope
    {
        public static CommandEnvelope<TCommand> For<TCommand>(TCommand command)
            where TCommand : Command =>
            new(command);
    }

    public record CommandEnvelope<TCommand> : IRequest
        where TCommand : Command
    {
        private readonly TCommand _command;

        internal CommandEnvelope(TCommand command) => _command = command;

        public TCommand AsCommand() => _command;

        public static implicit operator TCommand(CommandEnvelope<TCommand> envelope) => envelope.AsCommand();
    }
}