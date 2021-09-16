using System.Collections.Generic;

namespace StockTrader.Shared.Domain
{
    public interface IEventSource<out TEvent> : IEnumerable<TEvent>
        where TEvent : IDomainEvent
    {
        void MarkAsCommitted();
    }
}