using System;
using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Infrastructure.Messaging.Internal;

namespace StockTrader.Shared.Infrastructure.Messaging
{
    public static class EventPublisherDecorators
    {
        public static IEventPublisher<TEvent> OfEventType<TEvent>(
            this IEventPublisher publisher)
            where TEvent : class
        {
            return new TypedEventPublisher<TEvent>(publisher);
        }

        public static IEventPublisher<TSource> UseUpgrader<TSource, TDestination>(
            this IEventPublisher<TDestination> publisher,
            Func<TSource, TDestination> upgrader)
            where TSource : class
            where TDestination : class
        {
            return UseUpgrader(
                publisher,
                new InlineEventUpgrader<TSource, TDestination>(upgrader));
        }

        public static IEventPublisher<TSource> UseUpgrader<TSource, TDestination>(
            this IEventPublisher<TDestination> publisher,
            IEventUpgrader<TSource, TDestination> upgrader)
            where TSource : class
            where TDestination : class
        {
            return new UpgradingEventPublisher<TSource, TDestination>(upgrader, publisher);
        }
    }
}
