using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Application;
using SimpleCqrs.Common.Application.Messaging;
using SimpleCqrs.Common.Application.Persistence;
using SimpleCqrs.Portfolios.Domain;
using SimpleCqrs.Portfolios.Domain.Commands;

namespace SimpleCqrs.Portfolios.Application
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