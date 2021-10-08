using Microsoft.AspNetCore.Mvc.Testing;
using StockTrader.Shared.Domain;
using StockTrader.Testing.Api.Internal;

namespace StockTrader.Testing.Api
{
    public static class CommandTestBedBuilder
    {
        public static WithApiFactoryStep<TEntryPoint> WithApiFactory<TEntryPoint>(
            this TestBed _,
            WebApplicationFactory<TEntryPoint> apiFactory)
            where TEntryPoint : class
        {
            return new WithApiFactoryStep<TEntryPoint>(apiFactory);
        }

        public class WithApiFactoryStep<TEntryPoint>
            where TEntryPoint : class
        {
            private readonly WebApplicationFactory<TEntryPoint> _apiFactory;

            internal WithApiFactoryStep(WebApplicationFactory<TEntryPoint> apiFactory) =>
                _apiFactory = apiFactory;

            public ICommandTestBed<TAggregate> ForCommandOf<TAggregate>()
                where TAggregate : IAggregateRoot
            {
                return new CommandTestBed<TEntryPoint, TAggregate>(_apiFactory);
            }
        }
    }
}
