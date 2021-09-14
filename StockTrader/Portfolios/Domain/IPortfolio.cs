using StockTrader.Common.Domain;

namespace StockTrader.Portfolios.Domain
{
    public interface IPortfolio : 
        IAggregateRoot<PortfolioId>,
        IEventSourced<PortfolioEvent>,
        ISnapshotable<IPortfolio, PortfolioSnapshot>
    {
        void Execute(PortfolioCommand command);
    }
}