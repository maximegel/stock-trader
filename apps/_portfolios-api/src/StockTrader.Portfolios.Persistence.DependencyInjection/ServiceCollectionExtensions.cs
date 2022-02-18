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
                new PortfolioSqlRepository(provider.GetRequiredService<PortfoliosSqlWriteContext>())
                    .UseImmediateEventPublisher(
                        provider.GetRequiredService<IEventPublisher>()
                            .OfEventType<PortfolioIntegrationEvent>()
                            .UseUpgrader((PortfolioEventDescriptor e) => PortfolioIntegrationEvent.Create(e))));

            services.AddDbContext<PortfoliosSqlWriteContext>(options =>
                PortfoliosSqlWriteContextFactory.Configure(options, configuration));

            return services;
        }
    }
}
