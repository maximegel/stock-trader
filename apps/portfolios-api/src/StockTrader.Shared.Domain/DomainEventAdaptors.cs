using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public static class DomainEventAdaptors
    {
        public static IEnumerable<TDomainEvent> AsDomainEvents<TDomainEvent>(
            this IEnumerable<IDomainEventDescriptor<TDomainEvent>> descriptors)
            where TDomainEvent : IDomainEvent
        {
            return descriptors.Select(d => d.AsDomainEvent());
        }

        public static IEnumerable<IDomainEvent> AsDomainEvents(
            this IEnumerable<IDomainEventDescriptor> descriptors)
        {
            return descriptors.Select(d => d.AsDomainEvent());
        }
    }
}
