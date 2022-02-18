using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockTrader.Portfolios.Application;
using StockTrader.Portfolios.Projection.PortfolioDetails;

namespace StockTrader.Portfolios.Projection.Sql
{
    public class PortfoliosSqlReadModelFacade : IPortfoliosReadModelFacade
    {
        private readonly PortfoliosSqlReadContext _dbContext;

        public PortfoliosSqlReadModelFacade(PortfoliosSqlReadContext dbContext) =>
            _dbContext = dbContext;

        public IAsyncQueryable<PortfolioDetailView> PortfolioDetails =>
            _dbContext.PortfolioDetails
                .AsNoTracking()
                .AsAsyncEnumerable()
                .AsAsyncQueryable();
    }
}
