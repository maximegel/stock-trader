using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public class EventSource<TEvent> : IEventSource
        where TEvent : IDomainEvent
    {
        private readonly IAggregateRoot _aggregate;
        private readonly IEnumerable<TEvent> _events;

        public EventSource(IAggregateRoot aggregate)
            : this(aggregate, Enumerable.Empty<TEvent>())
        {
        }

        private EventSource(IAggregateRoot aggregate, IEnumerable<TEvent> events)
        {
            _aggregate = aggregate;
            _events = events.ToList();
        }

        IAggregateRoot IEventSource.Aggregate => _aggregate;

        public EventSource<TEvent> Append(params TEvent[] domainEvents) =>
            Append(domainEvents.AsEnumerable());

        public EventSource<TEvent> Append(IEnumerable<TEvent> domainEvents) =>
            new(_aggregate, _events.Concat(domainEvents));

        public EventSource<TEvent> Clear() =>
            new(_aggregate);

        IEnumerator<IDomainEvent> IEnumerable<IDomainEvent>.GetEnumerator() =>
            _events.OfType<IDomainEvent>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            ((IEnumerable)_events).GetEnumerator();
    }
}
