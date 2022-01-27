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
            IEnumerable<object> events,
            CancellationToken cancellationToken = default)
        {
            var tasks = events
                .Select(e => _mediator.Publish(e, cancellationToken))
                .ToArray();

            return Task.WhenAll(tasks);
        }
    }
}
