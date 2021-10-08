using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public abstract class EventSourcedAggregateRoot<TId, TEvent> : AggregateRoot<TId>,
        IEventSourced
        where TId : IIdentifier
        where TEvent : IDomainEvent
    {
        private EventSource<TEvent> _uncommittedEvents;

        protected EventSourcedAggregateRoot(TId id)
            : base(id)
        {
            _uncommittedEvents = new EventSource<TEvent>(this);
        }

        IEventSource IEventSourced.UncommittedEvents =>
            _uncommittedEvents;

        IEventSourced IEventSourced.ClearUncommittedEvents()
        {
            _uncommittedEvents = _uncommittedEvents.Clear();
            return this;
        }

        protected void Raise(params TEvent[] domainEvents) =>
            Raise(domainEvents.AsEnumerable());

        protected void Raise(IEnumerable<TEvent> domainEvents) =>
            _uncommittedEvents = _uncommittedEvents.Append(domainEvents);
    }
}
