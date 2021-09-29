using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Persistence;
using StockTrader.Portfolios.Persistence.Migrations;
using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Infrastructure.Persistence;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPortfoliosPersistence(
            this IServiceCollection services,
            IConfigurationSection configuration)
        {
            services.AddScoped(provider => 
                new PortfolioDbRepository(provider.GetRequiredService<PortfolioDbContext>())
                    .UseImmediateEventPublisher(provider.GetRequiredService<IEventPublisher>()));
            
            services.AddDbContext<PortfolioDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), 
                    opts => opts.MigrationsAssembly(typeof(PortfolioDbContextFactory).Assembly.FullName)));

            return services;
        }
    }
}