using Microsoft.EntityFrameworkCore;
using StockTrader.Portfolios.Persistence.Internal;

namespace StockTrader.Portfolios.Persistence
{
    public class PortfoliosSqlWriteContext : DbContext
    {
        public const string Schema = "portfolios_write";

        public PortfoliosSqlWriteContext(DbContextOptions<PortfoliosSqlWriteContext> options)
            : base(options)
        {
        }

        internal DbSet<PortfolioModel> Portfolios { get; init; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<PortfolioModel>(entity =>
            {
                entity.ToTable("Portfolio");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasMany(e => e.Holdings).WithOne(e => e.Portfolio).HasForeignKey(e => e.PortfolioId);
                entity.HasMany(e => e.Orders).WithOne(e => e.Portfolio).HasForeignKey(e => e.PortfolioId);
            });

            modelBuilder.Entity<HoldingModel>(entity =>
            {
                entity.ToTable("Holding");
                entity.HasKey(e => new { e.PortfolioId, e.Symbol });
            });

            modelBuilder.Entity<OrderModel>(entity =>
            {
                entity.ToTable("Order");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.TradeType).HasConversion<string>();
                entity.Property(e => e.OrderType).HasConversion<string>();
                entity.Property(e => e.PriceLimit).HasColumnType("decimal(19, 4)");
            });
        }
    }
}
