using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StockTrader.Shared.Application.Messaging;

namespace StockTrader.Shared.Infrastructure.Messaging
{
    public abstract class EventPublisher<TEvent> : IEventPublisher<TEvent>
        where TEvent : class
    {
        Task IEventPublisher<TEvent>.Publish(
            IEnumerable<TEvent> events,
            CancellationToken cancellationToken)
        {
            return Publish(events, cancellationToken);
        }

        Task IEventPublisher.Publish(
            IEnumerable<object> events,
            CancellationToken cancellationToken)
        {
            return Publish(events.OfType<TEvent>(), cancellationToken);
        }

        protected abstract Task Publish(
            IEnumerable<TEvent> events,
            CancellationToken cancellationToken = default);
    }
}
