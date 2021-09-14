using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Application.Messaging;
using SimpleCqrs.Common.Application.Persistence;
using SimpleCqrs.Portfolios.Domain;
using SimpleCqrs.Portfolios.Domain.Commands;

namespace SimpleCqrs.Portfolios.Application
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