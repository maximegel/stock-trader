using Microsoft.EntityFrameworkCore;
using StockTrader.Portfolios.Projection.PortfolioDetails;

namespace StockTrader.Portfolios.Projection.Sql
{
    public class PortfoliosSqlReadContext : DbContext
    {
        public const string Schema = "portfolios_read";

        public PortfoliosSqlReadContext(DbContextOptions<PortfoliosSqlReadContext> options)
            : base(options)
        {
        }

        internal DbSet<PortfolioDetailView> PortfolioDetails { get; init; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<PortfolioDetailView>(entity =>
            {
                entity.ToTable("PortfolioDetail");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
