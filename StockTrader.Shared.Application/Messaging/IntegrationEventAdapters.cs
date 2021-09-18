using System.Collections.Generic;
using System.Linq;
using StockTrader.Shared.Domain;

namespace StockTrader.Shared.Application.Messaging
{
    public static class IntegrationEventAdapters
    {
        public static IEnumerable<IDomainEvent> AsDomainEvents(
            this IEnumerable<IntegrationEvent> integrationEvents)
        {
            return integrationEvents
                .Select(integrationEvent => integrationEvent.AsDomainEvent());
        }

        public static IEnumerable<IntegrationEvent> ToIntegrationEvents(
            this IEventSource eventSource)
        {
            var aggregate = eventSource.Aggregate;
            return eventSource
                .OfType<IDomainEvent>()
                .Select(domainEvent => IntegrationEvent.Of(aggregate, domainEvent));
        }
    }
}