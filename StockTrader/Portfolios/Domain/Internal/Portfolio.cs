using System.Linq;
using StockTrader.Common.Domain;
using StockTrader.Portfolios.Domain.Internal.States;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal class Portfolio : EventSourcedAggregateRoot<PortfolioId, PortfolioEvent>,
        IPortfolio,
        IPortfolioBehavior,
        IPortfolioModel
    {
        public Portfolio(PortfolioId id) : base(id) {}

        public Holdings Holdings { get; private set; } = Holdings.Empty;
        private IPortfolioState State { get; set; } = new Nil();

        public void Execute(PortfolioCommand command)
        {
            var events = command.ExecuteOn(this).ToArray();
            Raise(events);
            Apply(events);
        }

        public void Open() =>
            State = State.Open();

        public void DebitShares(ShareCount shares) =>
            State = State.DebitShares(() => 
                Holdings = Holdings.Debit(shares));

        public void Close() =>
            State = State.Close();

        public IPortfolio RestoreSnapshot(PortfolioSnapshot snapshot) =>
            new Portfolio(Id)
            {
                State = PortfolioStateFactory.FromLabel(snapshot.Status),
                Holdings = new Holdings(
                    snapshot.Holdings.Select(
                        pair => new ShareCount(pair.Value, new Symbol(pair.Key))))
            };

        public PortfolioSnapshot TakeSnapshot() =>
            new(Id)
            {
                Status = State.Status,
                Holdings = Holdings.ToDictionary(
                    shares => shares.Symbol.ToString(), 
                    shares => shares.ToInt())
            };

        protected override void Apply(PortfolioEvent domainEvent) =>
            domainEvent.ApplyTo(this);
    }
}