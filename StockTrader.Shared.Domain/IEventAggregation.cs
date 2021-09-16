using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public interface IEventAggregation<in TEvent>
        where TEvent : IDomainEvent
    {
        IEventAggregation<TEvent> Apply(TEvent domainEvent);

        IEventAggregation<TEvent> Apply(params TEvent[] domainEvents) =>
            Apply(domainEvents.AsEnumerable());
            
        IEventAggregation<TEvent> Apply(IEnumerable<TEvent> domainEvents) =>
            domainEvents.Aggregate(this, (self, e) => self.Apply(e));
    }
}