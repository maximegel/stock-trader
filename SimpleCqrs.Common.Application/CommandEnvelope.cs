using System.Collections.Generic;
using MediatR;

namespace SimpleCqrs.Common.Application
{
    public abstract class CommandEnvelope<TCommand> : IRequest
    {
        public CommandEnvelope(TCommand data) => Data = data;

        public TCommand Data { get; }

        public IDictionary<string, string> Metadata { get; } = new Dictionary<string, string>();

        public static implicit operator TCommand(CommandEnvelope<TCommand> envelope) => envelope.Data;
    }
}