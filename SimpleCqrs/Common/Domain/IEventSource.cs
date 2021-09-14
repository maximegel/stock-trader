using System.Collections.Generic;

namespace SimpleCqrs.Common.Domain
{
    public interface IEventSource<out TEvent> : IEnumerable<TEvent>
        where TEvent : IDomainEvent
    {
        void MarkAsCommitted();
    }
}