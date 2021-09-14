using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Application.Messaging;
using SimpleCqrs.Common.Application.Persistence;
using SimpleCqrs.Portfolios.Domain;
using SimpleCqrs.Portfolios.Domain.Commands;

namespace SimpleCqrs.Portfolios.Application
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