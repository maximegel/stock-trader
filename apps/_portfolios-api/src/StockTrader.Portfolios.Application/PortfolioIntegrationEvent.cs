using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using StockTrader.Portfolios.Domain;
using StockTrader.Shared.Application.Messaging;

namespace StockTrader.Portfolios.Application
{
    public abstract record PortfolioIntegrationEvent(
        string AggregateId)
        : IIntegrationEvent
    {
        public static IEnumerable<PortfolioIntegrationEvent> CreateRange(
            PortfolioId aggregateId,
            params PortfolioEvent[] domainEvents)
        {
            return domainEvents.Select(e => Create(aggregateId, e));
        }

        public static PortfolioIntegrationEvent Create(
            PortfolioId aggregateId,
            PortfolioEvent domainEvent)
        {
            return Create(new PortfolioEventDescriptor(aggregateId, domainEvent));
        }

        public static PortfolioIntegrationEvent Create(PortfolioEventDescriptor descriptor)
        {
            var (aggregateId, data, metadata) = descriptor;
            var type = typeof(PortfolioIntegrationEvent<>).MakeGenericType(data.GetType());
            var instance = Activator.CreateInstance(type, aggregateId, data, metadata)!;
            return (PortfolioIntegrationEvent)instance;
        }
    }

    public record PortfolioIntegrationEvent<TDomainEvent>(
        string AggregateId,
        TDomainEvent Data,
        PortfolioEventMetadata Metadata)
        : PortfolioIntegrationEvent(AggregateId);
}
