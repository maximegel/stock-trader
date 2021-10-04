using System.Collections.Generic;

namespace StockTrader.Shared.Domain
{
    public interface IEventSource : IEnumerable<IDomainEvent>
    {
        IAggregateRoot Aggregate { get; }
    }
}