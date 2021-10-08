using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StockTrader.Shared.Application.Messaging;

namespace StockTrader.Shared.Infrastructure.Messaging.MediatR
{
    public class MediatorEventPublisher : IEventPublisher
    {
        private readonly IMediator _mediator;

        public MediatorEventPublisher(IMediator mediator) =>
            _mediator = mediator;

        public Task Publish(
            IEnumerable<IntegrationEvent> events,
            CancellationToken cancellationToken = default)
        {
            var tasks = events
                .Select(integrationEvent => _mediator.Publish(integrationEvent, cancellationToken))
                .ToArray();

            return Task.WhenAll(tasks);
        }
    }
}
