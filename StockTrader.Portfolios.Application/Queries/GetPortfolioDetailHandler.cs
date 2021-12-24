using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StockTrader.Portfolios.Projection.PortfolioDetails;
using StockTrader.Shared.Application.Messaging;

namespace StockTrader.Portfolios.Application.Queries
{
    public class GetPortfolioDetailHandler :
        QueryHandler<GetPortfolioDetail, PortfolioDetailView>
    {
        private readonly IPortfoliosReadModelFacade _readModel;

        public GetPortfolioDetailHandler(IPortfoliosReadModelFacade readModel) =>
            _readModel = readModel;

        protected override async Task<PortfolioDetailView> Handle(
            GetPortfolioDetail query,
            CancellationToken cancellationToken)
        {
            var result = await _readModel.PortfolioDetails
                .FirstOrDefaultAsync(d => d.Id.ToString() == query.PortfolioId, cancellationToken);
            return result ?? throw new ApplicationException();
        }
    }

    public record GetPortfolioDetail(string PortfolioId) :
        Query<PortfolioDetailView>;
}
