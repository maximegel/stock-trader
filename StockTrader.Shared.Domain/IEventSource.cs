using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public interface IEventSource<out TEvent> : 
        IEventSource, IEnumerable<TEvent>
        where TEvent : IDomainEvent
    { }
    
    public interface IEventSource : IEnumerable
    {
        IAggregateRoot Aggregate { get; }

        IEnumerable<IDomainEvent> AsEnumerable() =>
            this.OfType<IDomainEvent>();
    }
}