using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public interface IPortfolio : 
        IAggregateRoot<PortfolioId>,
        IEventSourced,
        ISnapshotable<IPortfolio, PortfolioSnapshot>
    {
        void Execute(PortfolioCommand command);
    }
}