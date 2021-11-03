using Microsoft.Extensions.Configuration;
using StockTrader.Portfolios.Application;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Persistence;
using StockTrader.Portfolios.Persistence.Migrations;
using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Infrastructure.Messaging;
using StockTrader.Shared.Infrastructure.Persistence;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPortfoliosPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped(provider =>
                new PortfolioDbRepository(provider.GetRequiredService<PortfoliosWriteDbContext>())
                    .UseImmediateEventPublisher(
                        provider.GetRequiredService<IEventPublisher<PortfolioIntegrationEvent>>()
                            .UseUpgrader((PortfolioEventDescriptor e) => PortfolioIntegrationEvent.From(e))));

            services.AddDbContext<PortfoliosWriteDbContext>(options =>
                PortfoliosWriteDbContextFactory.Configure(options, configuration));

            return services;
        }
    }
}
