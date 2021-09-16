using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Domain;

namespace StockTrader.Shared.Infrastructure.Messaging.MediatR
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