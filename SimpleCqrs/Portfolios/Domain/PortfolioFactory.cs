using SimpleCqrs.Common.Domain;
using SimpleCqrs.Portfolios.Domain.Commands;
using SimpleCqrs.Portfolios.Domain.Internal;

namespace SimpleCqrs.Portfolios.Domain
{
    public static class PortfolioFactory
    {
        public static IPortfolio Load(Identifier id) =>
            new Portfolio((PortfolioId) id);

        public static IPortfolio Open(OpenPortfolio command)
        {
            var portfolioId = PortfolioId.Parse(command.AggregateId);
            var portfolio = new Portfolio(portfolioId);
            portfolio.Execute(command);
            return portfolio;
        }
    }
}