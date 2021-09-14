using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Portfolios.Domain
{
    public interface IPortfolio : 
        IAggregateRoot<PortfolioId>,
        IEventSourced<PortfolioEvent>,
        ISnapshotable<IPortfolio, PortfolioSnapshot>
    {
        void Execute(PortfolioCommand command);
    }
}