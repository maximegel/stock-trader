using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public interface IEventAggregation
    {
        IEventAggregation Apply(IDomainEvent domainEvent);

        IEventAggregation Apply(params IDomainEvent[] domainEvents) =>
            Apply(domainEvents.AsEnumerable());
            
        IEventAggregation Apply(IEnumerable<IDomainEvent> domainEvents) =>
            domainEvents.Aggregate(this, (self, e) => self.Apply(e));
    }
}