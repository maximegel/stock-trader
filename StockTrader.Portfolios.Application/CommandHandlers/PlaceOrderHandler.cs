using System;
using System.Threading;
using System.Threading.Tasks;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Application.Persistence;

namespace StockTrader.Portfolios.Application.CommandHandlers
{
    public class PlaceOrderHandler : CommandHandler<PlaceOrder>
    {
        private readonly IRepository<IPortfolio> _repository;

        public PlaceOrderHandler(IRepository<IPortfolio> repository) => 
            _repository = repository;

        protected override async Task Handle(PlaceOrder command, CancellationToken cancellationToken)
        {
            var portfolioId = (PortfolioId)command.AggregateId;
            var portfolio = await _repository.Find(portfolioId, cancellationToken)
                            ?? throw new Exception("Aggregate not found.");
            portfolio.Execute(command);
            await _repository.Save(portfolio, cancellationToken);
        }
    }
}