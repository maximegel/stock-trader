using System.Threading;
using System.Threading.Tasks;
using StockTrader.Portfolios.Application;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Shared.Application.Messaging;

namespace StockTrader.Portfolios.Projection.PortfolioDetails
{
    public class PortfolioDetailProjector :
        IEventHandler<PortfolioIntegrationEvent<PortfolioOpened>>,
        IEventHandler<PortfolioIntegrationEvent<PortfolioClosed>>
    {
        private readonly IPortfolioDetailProjection _projection;

        public PortfolioDetailProjector(IPortfolioDetailProjection projection) =>
            _projection = projection;

        public async Task Handle(
            PortfolioIntegrationEvent<PortfolioOpened> e,
            CancellationToken cancellationToken)
        {
            var (aggregateId, data, _) = e;
            await _projection.Create(aggregateId, data.Name, cancellationToken);
        }

        public async Task Handle(
            PortfolioIntegrationEvent<PortfolioClosed> e,
            CancellationToken cancellationToken)
        {
            var (aggregateId, _, _) = e;
            await _projection.Delete(aggregateId, cancellationToken);
        }
    }
}
