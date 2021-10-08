using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public interface IEventAggregation<out TSelf, in TEvent>
        where TSelf : IEventAggregation<TSelf, TEvent>
        where TEvent : IDomainEvent
    {
        TSelf Apply(TEvent domainEvent);

        TSelf Apply(params TEvent[] domainEvents) =>
            Apply(domainEvents.AsEnumerable());

        TSelf Apply(IEnumerable<TEvent> domainEvents) =>
            (TSelf)domainEvents.Aggregate(this, (agg, e) => agg.Apply(e));
    }
}
