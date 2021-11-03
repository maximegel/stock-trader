using System.Collections.Generic;

namespace StockTrader.Shared.Domain
{
    public interface IEventSourced<out TAggregate>
        where TAggregate : IAggregateRoot
    {
        IEnumerable<IDomainEventDescriptor> UncommittedEvents { get; }

        TAggregate MarkAsCommitted();
    }

    public interface IEventSourced<out TAggregate, out TEvent> :
        IEventSourced<TAggregate>
        where TAggregate : IAggregateRoot
        where TEvent : IDomainEventDescriptor
    {
        new IEnumerable<TEvent> UncommittedEvents { get; }
    }
}
