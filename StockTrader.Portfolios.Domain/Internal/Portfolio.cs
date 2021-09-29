﻿using System.Linq;
using StockTrader.Portfolios.Domain.Internal.States;
using StockTrader.Shared.Domain;

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

        public IPortfolio Execute(PortfolioCommand command)
        {
            var events = command.ExecuteOn(this).ToArray();
            Raise(events);
            return (this as IPortfolio).Apply(events);
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

        public IPortfolio Apply(PortfolioEvent domainEvent)
        {
            domainEvent.ApplyTo(this);
            return this;
        }
    }
}