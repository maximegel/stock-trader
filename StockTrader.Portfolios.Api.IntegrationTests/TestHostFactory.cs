using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTrader.Api;
using StockTrader.Portfolios.Persistence;
using StockTrader.Portfolios.Projection.Sql;

namespace StockTrader.Portfolios.Api.IntegrationTests
{
    public class TestHostFactory : WebApplicationFactory<Startup>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = builder.Build();
            CreateDatabases(host);
            host.Start();
            return host;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.UseEnvironment("Test");
        }

        private static void CreateDatabases(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var readContext = services.GetRequiredService<PortfoliosSqlReadContext>();
            var writeContext = services.GetRequiredService<PortfoliosSqlWriteContext>();
            CreateDatabases(readContext, writeContext);
        }

        private static void CreateDatabases(params DbContext[] dbContexts)
        {
            dbContexts
                .GroupBy(context => context.Database.GetConnectionString())
                .ToList()
                .ForEach(contexts =>
                {
                    // Deletes the database once.
                    contexts.First().Database.EnsureDeleted();

                    // Then applies the migrations for all contexts within that database.
                    contexts.ToList().ForEach(ctx => ctx.Database.Migrate());
                });
        }
    }
}
