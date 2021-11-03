using System;
using MediatR;
using StockTrader.Portfolios.Domain;
using StockTrader.Shared.Application.Messaging;

namespace StockTrader.Portfolios.Application
{
    public abstract record PortfolioIntegrationEvent(
        string AggregateId)
        : IIntegrationEvent, INotification
    {
        public static PortfolioIntegrationEvent From(PortfolioEventDescriptor descriptor)
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
