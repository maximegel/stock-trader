using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Application;
using SimpleCqrs.Common.Domain;
using SimpleCqrs.Portfolios.Domain;

namespace SimpleCqrs.Portfolios.Application
{
    public record OpenPortfolio(string Name) : Command
    {
        public readonly string PortfolioId = Guid.NewGuid().ToString();
    }

    public class OpenPortfolioHandler : CommandHandler<OpenPortfolio>
    {
        private readonly IWriteOnlyRepository<Portfolio> _repository;

        public OpenPortfolioHandler(IWriteOnlyRepository<Portfolio> repository) =>
            _repository = repository;

        protected override async Task Handle(OpenPortfolio command, CancellationToken cancellationToken)
        {
            var portfolioId = PortfolioId.Parse(command.PortfolioId);
            var portfolio = new Portfolio(portfolioId, command.Name);
            await _repository.Save(portfolio, cancellationToken);
        }
    }
}