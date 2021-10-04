using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public static class PortfolioFactory
    {
        public static IPortfolio LoadFromHistory(PortfolioId id, params PortfolioEvent[] events) =>
            Load(id).Apply(events);
        
        public static IPortfolio LoadFromSnapshot(PortfolioId id, PortfolioSnapshot snapshot) => 
            Load(id).RestoreSnapshot(snapshot);

        public static IPortfolio Open(OpenPortfolio command)
        {
            var id = PortfolioId.Parse(command.AggregateId);
            return new Portfolio(id).Execute(command);
        }
        
        private static IPortfolio Load(IIdentifier id) =>
            new Portfolio((PortfolioId) id);
    }
}