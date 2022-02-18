using System;
using System.Linq;
using StockTrader.Portfolios.Domain.Failures;
using StockTrader.Portfolios.Domain.Internal.States;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal class Portfolio :
        EventSourcedAggregateRoot<IPortfolio, PortfolioId, PortfolioEventDescriptor>,
        IPortfolio
    {
        private PortfolioState _state = new NotOpened();

        public Portfolio(PortfolioId id)
            : base(id)
        {
        }

        private PortfolioModel Model => new(_state);

        public IPortfolio Execute(PortfolioCommand command)
        {
            try
            {
                var events = command.ExecuteOn(Model).ToArray();
                (this as IPortfolio).Apply(events);
                Raise(events.Select(e => e.ToDescriptor(this, command)));
            }
            catch (Exception)
            {
                Raise(new PortfolioFailedUnexpectedly().ToDescriptor(this, command));
            }

            return this;
        }

        public IPortfolio RestoreSnapshot(PortfolioSnapshot snapshot) =>
            new Portfolio(Id) { _state = PortfolioState.FromSnapshot(snapshot) };

        public PortfolioSnapshot TakeSnapshot() =>
            _state.ToSnapshot();

        public IPortfolio Apply(PortfolioEvent domainEvent)
        {
            _state = domainEvent.ApplyTo(_state);
            return this;
        }
    }
}
