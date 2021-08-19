using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Portfolios.Domain
{
    public class Portfolio : AggregateRoot<PortfolioId>
    {
        public Portfolio(PortfolioId id, string name) : base(id) => Name = name;

        private Portfolio() : base(PortfolioId.Generate()) { }

        public string Name { get; }
    }

    public class PortfolioId : Uuid<PortfolioId> { }
}