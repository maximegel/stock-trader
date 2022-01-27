using System.Linq;
using StockTrader.Portfolios.Projection.PortfolioDetails;

namespace StockTrader.Portfolios.Application
{
    public interface IPortfoliosReadModelFacade
    {
        IAsyncQueryable<PortfolioDetailView> PortfolioDetails { get; }
    }
}
