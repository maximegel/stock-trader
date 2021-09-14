using System.Collections.Generic;
using System.Linq;

namespace SimpleCqrs.Common.Domain
{
    public abstract class EventSourcedAggregateRoot<TId, TEvent> : AggregateRoot<TId>,
        IEventSourced<TEvent>,
        IEventAggregation<TEvent>
        where TId : Identifier
        where TEvent : IDomainEvent
    {
        private readonly EventSource<TEvent> _uncommittedEvents = new();

        protected EventSourcedAggregateRoot(TId id) : base(id) { }
        
        IEventSource<TEvent> IEventSourced<TEvent>.UncommittedEvents =>
            _uncommittedEvents;

        IEventAggregation<TEvent> IEventAggregation<TEvent>.Apply(TEvent domainEvent)
        {
            Apply(domainEvent);
            return this;
        }

        protected void Raise(params TEvent[] domainEvents) =>
            Raise(domainEvents.AsEnumerable());

        protected void Raise(IEnumerable<TEvent> domainEvents)
        {
            var events = domainEvents as TEvent[] ?? domainEvents.ToArray();
            _uncommittedEvents.Append(events);
        }

        protected void Apply(params TEvent[] domainEvents) =>
            Apply(domainEvents.AsEnumerable());

        protected void Apply(IEnumerable<TEvent> domainEvents) =>
            AsProjection().Apply(domainEvents);

        protected abstract void Apply(TEvent domainEvent);

        private IEventAggregation<TEvent> AsProjection() => this;
    }
}