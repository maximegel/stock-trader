using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StockTrader.Portfolios.Persistence.Migrations
{
    public class PortfoliosSqlWriteContextFactory : IDesignTimeDbContextFactory<PortfoliosSqlWriteContext>
    {
        public static TOptionsBuilder Configure<TOptionsBuilder>(
            TOptionsBuilder options,
            IConfiguration configuration)
            where TOptionsBuilder : DbContextOptionsBuilder
        {
            return (TOptionsBuilder)options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                opts => opts
                    .MigrationsAssembly(typeof(PortfoliosSqlWriteContextFactory).Assembly.FullName)
                    .MigrationsHistoryTable("__MigrationsHistory", PortfoliosSqlWriteContext.Schema));
        }

        public PortfoliosSqlWriteContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var options = Configure(
                    new DbContextOptionsBuilder<PortfoliosSqlWriteContext>(),
                    configuration)
                .Options;

            return new PortfoliosSqlWriteContext(options);
        }
    }
}
