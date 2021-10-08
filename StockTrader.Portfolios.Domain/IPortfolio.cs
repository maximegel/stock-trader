using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public interface IPortfolio :
        IAggregateRoot<PortfolioId>,
        IEventSourced,
        IEventAggregation<IPortfolio, PortfolioEvent>,
        ISnapshotable<IPortfolio, PortfolioSnapshot>
    {
        IPortfolio Execute(PortfolioCommand command);
    }
}
