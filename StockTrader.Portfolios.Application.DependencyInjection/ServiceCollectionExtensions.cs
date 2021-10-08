using MediatR;
using StockTrader.Portfolios.Application;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPortfoliosApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(Library.Assembly);
            return services;
        }
    }
}
