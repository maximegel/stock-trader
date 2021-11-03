using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public interface IProjectable<out TAggregate, in TEvent>
        where TAggregate : IAggregateRoot, IProjectable<TAggregate, TEvent>
        where TEvent : IDomainEvent
    {
        TAggregate Apply(TEvent domainEvent);

        TAggregate Apply(params TEvent[] domainEvents) =>
            Apply(domainEvents.AsEnumerable());

        TAggregate Apply(IEnumerable<TEvent> domainEvents) =>
            (TAggregate)domainEvents.Aggregate(this, (agg, e) => agg.Apply(e));
    }
}
