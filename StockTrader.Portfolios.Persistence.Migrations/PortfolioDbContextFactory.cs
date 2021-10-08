using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StockTrader.Portfolios.Persistence.Migrations
{
    public class PortfolioDbContextFactory : IDesignTimeDbContextFactory<PortfolioDbContext>
    {
        public PortfolioDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var options = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName))
                .Options;

            return new PortfolioDbContext(options);
        }
    }
}
