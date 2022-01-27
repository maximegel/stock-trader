using System.Threading;
using System.Threading.Tasks;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Application.Persistence;

namespace StockTrader.Portfolios.Application.Commands
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
