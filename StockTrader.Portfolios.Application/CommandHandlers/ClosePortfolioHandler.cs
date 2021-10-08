using System;
using System.Threading;
using System.Threading.Tasks;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Application.Persistence;

namespace StockTrader.Portfolios.Application.CommandHandlers
{
    public class ClosePortfolioHandler : CommandHandler<ClosePortfolio>
    {
        private readonly IRepository<IPortfolio> _repository;

        public ClosePortfolioHandler(IRepository<IPortfolio> repository) =>
            _repository = repository;

        protected override async Task Handle(ClosePortfolio command, CancellationToken cancellationToken)
        {
            var portfolioId = PortfolioId.Parse(command.AggregateId);
            var portfolio = await _repository.Find(portfolioId, cancellationToken)
                            ?? throw new Exception("Aggregate not found.");
            portfolio.Execute(command);
            await _repository.Save(portfolio, cancellationToken);
        }
    }
}
