using Microsoft.EntityFrameworkCore;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Persistence;
using StockTrader.Shared.Application.Persistence;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPortfoliosRepositories(
            this IServiceCollection services) =>
            services.AddScoped<IRepository<IPortfolio>, PortfolioDbRepository>();

        public static IServiceCollection AddPortfoliosStorage(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddScoped(provider => 
                new PortfolioDbRepository(provider.GetRequiredService<PortfolioDbContext>())
                    .UseImmediateEventPublisher(provider.GetRequiredService<IEventPublisher>()));
            
            return services.AddDbContext<PortfolioDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}