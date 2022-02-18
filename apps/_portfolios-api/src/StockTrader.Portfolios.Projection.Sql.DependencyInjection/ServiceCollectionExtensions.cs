using MediatR;
using Microsoft.Extensions.Configuration;
using StockTrader.Portfolios.Application;
using StockTrader.Portfolios.Projection.PortfolioDetails;
using StockTrader.Portfolios.Projection.Sql;
using StockTrader.Portfolios.Projection.Sql.Migrations;
using StockTrader.Portfolios.Projection.Sql.PortfolioDetails;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPortfoliosProjectionSql(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(StockTrader.Portfolios.Projection.Library.Assembly);

            services.AddScoped<IPortfolioDetailProjection, PortfolioDetailSqlProjection>();
            services.AddScoped<IPortfoliosReadModelFacade, PortfoliosSqlReadModelFacade>();

            services.AddDbContext<PortfoliosSqlReadContext>(options =>
                PortfoliosSqlReadContextFactory.Configure(options, configuration));

            return services;
        }
    }
}
