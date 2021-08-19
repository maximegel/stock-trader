using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleCqrs.Portfolios.Persistence.DataModels
{
    internal class PortfolioDataConfig : IEntityTypeConfiguration<PortfolioData>
    {
        public void Configure(EntityTypeBuilder<PortfolioData> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}