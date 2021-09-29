using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockTrader.Portfolios.Persistence;

namespace StockTrader.Portfolios.Api.IntegrationTests
{
    public class PortfoliosDataInitializer
    {
        private static readonly object InitializeLock = new();
        private static bool _initialized;

        public static void Initialize(IServiceProvider services)
        {
            if (_initialized) return;

            lock (InitializeLock)
            {
                var dbContext = services.GetRequiredService<PortfolioDbContext>();
                var logger = services.GetRequiredService<ILogger<PortfoliosDataInitializer>>();
                try
                {
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to initialize database");
                    throw;
                }
                _initialized = true;
            }
        }
    }
}