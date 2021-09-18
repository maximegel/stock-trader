using System.Collections;
using System.Collections.Generic;

namespace StockTrader.Shared.Domain
{
    public interface IEventSource<out TEvent> : 
        IEventSource, IEnumerable<TEvent> { }
    
    public interface IEventSource : IEnumerable
    {
        IAggregateRoot Aggregate { get; }
    }
}