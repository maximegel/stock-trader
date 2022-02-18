using System.Threading;
using System.Threading.Tasks;

namespace StockTrader.Portfolios.Projection.PortfolioDetails
{
    public interface IPortfolioDetailProjection
    {
        Task Create(
            string portfolioId,
            string? name,
            CancellationToken cancellationToken = default);

        Task Delete(string portfolioId, CancellationToken cancellationToken = default);
    }
}
