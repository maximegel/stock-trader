using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public abstract class EventSourcedAggregateRoot<TSelf, TId, TEvent> : AggregateRoot<TSelf, TId>,
        IEventSourced<TSelf, TEvent>
        where TSelf : class, IAggregateRoot<TId>, IEventSourced<TSelf, TEvent>
        where TId : IIdentifier
        where TEvent : IDomainEventDescriptor
    {
        private UncommittedEvents<TEvent> _uncommittedEvents;

        protected EventSourcedAggregateRoot(TId id)
            : base(id)
        {
            _uncommittedEvents = new UncommittedEvents<TEvent>(this);
        }

        IEnumerable<IDomainEventDescriptor> IEventSourced<TSelf>.UncommittedEvents =>
            _uncommittedEvents.OfType<IDomainEventDescriptor>();

        IEnumerable<TEvent> IEventSourced<TSelf, TEvent>.UncommittedEvents =>
            _uncommittedEvents;

        TSelf IEventSourced<TSelf>.MarkAsCommitted()
        {
            _uncommittedEvents = _uncommittedEvents.Clear();
            return AsSelf();
        }

        protected void Raise(params TEvent[] events) =>
            Raise(events.AsEnumerable());

        protected void Raise(IEnumerable<TEvent> events) =>
            _uncommittedEvents = _uncommittedEvents.Append(events);

        private TSelf AsSelf() =>
            this as TSelf
            ?? throw new InvalidCastException(
                $"{GetType().Name} must be assignable to {typeof(TSelf).Name}.");
    }
}
