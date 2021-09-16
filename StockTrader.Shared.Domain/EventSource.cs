using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public class EventSource<TEvent> : IEventSource<TEvent>
        where TEvent : IDomainEvent
    {
        private readonly List<TEvent> _events;
        
        public EventSource() : this(Enumerable.Empty<TEvent>())
        {
        }

        private EventSource(IEnumerable<TEvent> events)
        {
            _events = events.ToList();
        }
        
        public void Append(params TEvent[] domainEvents) =>
            _events.AddRange(domainEvents);

        public void Append(IEnumerable<TEvent> domainEvents) =>
            _events.AddRange(domainEvents);
        
        public void MarkAsCommitted() => 
            _events.Clear();
        
        public IEnumerator<TEvent> GetEnumerator() => _events.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _events).GetEnumerator();
    }
}