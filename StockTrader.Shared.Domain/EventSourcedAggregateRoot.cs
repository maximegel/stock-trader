using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public abstract class EventSourcedAggregateRoot<TId, TEvent> : AggregateRoot<TId>,
        IEventSourced,
        IEventAggregation
        where TId : IIdentifier
        where TEvent : IDomainEvent
    {
        private EventSource<TEvent> _uncommittedEvents;

        protected EventSourcedAggregateRoot(TId id) : base(id) =>
            _uncommittedEvents = new EventSource<TEvent>(this);

        IEventAggregation IEventAggregation.Apply(IDomainEvent domainEvent)
        {
            if (domainEvent is TEvent aggregateEvent)
                Apply(aggregateEvent);
            return this;
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

        protected void Apply(params TEvent[] domainEvents) =>
            Apply(domainEvents.AsEnumerable());

        protected void Apply(IEnumerable<TEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
                Apply(domainEvent);
        }

        protected abstract void Apply(TEvent domainEvent);
    }
}