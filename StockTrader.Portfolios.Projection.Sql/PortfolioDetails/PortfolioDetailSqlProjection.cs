using System;
using System.Threading;
using System.Threading.Tasks;
using StockTrader.Portfolios.Projection.PortfolioDetails;

namespace StockTrader.Portfolios.Projection.Sql.PortfolioDetails
{
    public class PortfolioDetailSqlProjection : IPortfolioDetailProjection
    {
        private readonly PortfoliosSqlReadContext _readContext;

        public PortfolioDetailSqlProjection(PortfoliosSqlReadContext readContext) =>
            _readContext = readContext;

        public async Task Create(
            string portfolioId,
            string? name,
            CancellationToken cancellationToken = default)
        {
            _readContext.PortfolioDetails.Add(new PortfolioDetailView
            {
                Id = Guid.Parse(portfolioId),
                Name = name,
            });
            await _readContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(
            string portfolioId,
            CancellationToken cancellationToken = default)
        {
            var found = await _readContext.PortfolioDetails.FindAsync(
                new object[] { Guid.Parse(portfolioId) },
                cancellationToken);

            if (found != null)
            {
                _readContext.PortfolioDetails.Remove(found);
            }

            await _readContext.SaveChangesAsync(cancellationToken);
        }
    }
}
