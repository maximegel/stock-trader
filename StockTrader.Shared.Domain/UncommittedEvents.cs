using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public class UncommittedEvents<TEvent> : IEnumerable<TEvent>
    {
        private readonly IAggregateRoot _aggregate;
        private readonly IEnumerable<TEvent> _events;

        public UncommittedEvents(IAggregateRoot aggregate)
            : this(aggregate, Enumerable.Empty<TEvent>())
        {
        }

        private UncommittedEvents(IAggregateRoot aggregate, IEnumerable<TEvent> events)
        {
            _aggregate = aggregate;
            _events = events.ToList();
        }

        public UncommittedEvents<TEvent> Append(params TEvent[] domainEvents) =>
            Append(domainEvents.AsEnumerable());

        public UncommittedEvents<TEvent> Append(IEnumerable<TEvent> domainEvents) =>
            new(_aggregate, _events.Concat(domainEvents));

        public UncommittedEvents<TEvent> Clear() =>
            new(_aggregate);

        IEnumerator<TEvent> IEnumerable<TEvent>.GetEnumerator() =>
            _events.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            ((IEnumerable)_events).GetEnumerator();
    }
}
