using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Portfolios.Persistence.DataModels;

namespace SimpleCqrs.Portfolios.Persistence
{
    internal class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) :
            base(options) { }

        public DbSet<PortfolioData> Portfolios { get; init; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PortfolioDataConfig());
        }
    }
}