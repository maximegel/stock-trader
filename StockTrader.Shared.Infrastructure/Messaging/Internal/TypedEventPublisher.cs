using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StockTrader.Shared.Application.Messaging;

namespace StockTrader.Shared.Infrastructure.Messaging.Internal
{
    internal class TypedEventPublisher<TEvent> : EventPublisher<TEvent>
        where TEvent : class
    {
        private readonly IEventPublisher _decorated;

        public TypedEventPublisher(IEventPublisher decorated) =>
            _decorated = decorated;

        protected override Task Publish(
            IEnumerable<TEvent> events,
            CancellationToken cancellationToken = default)
        {
            return _decorated.Publish(events, cancellationToken);
        }
    }
}
