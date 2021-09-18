using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public static class PortfolioFactory
    {
        public static IPortfolio Load(IIdentifier id) =>
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