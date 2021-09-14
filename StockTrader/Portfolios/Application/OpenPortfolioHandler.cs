using System.Threading;
using System.Threading.Tasks;
using StockTrader.Common.Application.Messaging;
using StockTrader.Common.Application.Persistence;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Commands;

namespace StockTrader.Portfolios.Application
{
    public class OpenPortfolioHandler : CommandHandler<OpenPortfolio>
    {
        private readonly IRepository<IPortfolio> _repository;

        public OpenPortfolioHandler(IRepository<IPortfolio> repository) =>
            _repository = repository;

        protected override async Task Handle(OpenPortfolio command, CancellationToken cancellationToken)
        {
            var portfolio = PortfolioFactory.Open(command);
            await _repository.Save(portfolio, cancellationToken);
        }
    }
}