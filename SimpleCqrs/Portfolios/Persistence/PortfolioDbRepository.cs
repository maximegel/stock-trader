using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Common.Persistence;
using SimpleCqrs.Portfolios.Domain;

namespace SimpleCqrs.Portfolios.Persistence
{
    internal class PortfolioDbRepository : DbWriteOnlyRepository<Portfolio>
    {
        public PortfolioDbRepository(DbContext context) : base(context) { }
    }
}