using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StockTrader.Common.Application.Messaging;
using StockTrader.Common.Domain;

namespace StockTrader.Common.Infrastructure.Messaging.MediatR
{
    public class MediatorCommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public MediatorCommandBus(IMediator mediator) => _mediator = mediator;

        public Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand =>
            _mediator.Send(CommandEnvelope.For(command), cancellationToken);
    }
}