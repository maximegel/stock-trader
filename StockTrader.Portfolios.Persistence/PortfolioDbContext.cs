using Microsoft.EntityFrameworkCore;
using StockTrader.Portfolios.Persistence.DataModels;

namespace StockTrader.Portfolios.Persistence
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) :
            base(options) { }

        internal DbSet<PortfolioData> Portfolios { get; init; } = null!;

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<PortfolioData>(entity =>
            {
                entity.ToTable("Portfolio");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Status).HasConversion<string>();
                entity.HasMany(e => e.Holdings).WithOne(e => e.Portfolio).HasForeignKey(e => e.PortfolioId);
                entity.HasMany(e => e.Orders).WithOne(e => e.Portfolio).HasForeignKey(e => e.PortfolioId);
            });
            
            model.Entity<HoldingData>(entity =>
            {
                entity.ToTable("Holding");
                entity.HasKey(e => new {e.PortfolioId, e.Symbol});
            });
            
            model.Entity<OrderData>(entity =>
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