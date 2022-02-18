using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StockTrader.Portfolios.Projection.Sql.Migrations
{
    public class PortfoliosSqlReadContextFactory : IDesignTimeDbContextFactory<PortfoliosSqlReadContext>
    {
        public static TOptionsBuilder Configure<TOptionsBuilder>(
            TOptionsBuilder options,
            IConfiguration configuration)
            where TOptionsBuilder : DbContextOptionsBuilder
        {
            return (TOptionsBuilder)options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                opts => opts
                    .MigrationsAssembly(typeof(PortfoliosSqlReadContextFactory).Assembly.FullName)
                    .MigrationsHistoryTable("__MigrationsHistory", PortfoliosSqlReadContext.Schema));
        }

        public PortfoliosSqlReadContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var options = Configure(
                    new DbContextOptionsBuilder<PortfoliosSqlReadContext>(),
                    configuration)
                .Options;

            return new PortfoliosSqlReadContext(options);
        }
    }
}
