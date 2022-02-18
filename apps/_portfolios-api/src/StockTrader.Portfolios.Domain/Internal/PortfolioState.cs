using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal abstract record PortfolioState
    {
        private static readonly IReadOnlyDictionary<string, Func<PortfolioState>> Factories =
            typeof(PortfolioState).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(PortfolioState)))
                .Where(t => !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(PortfolioState<>).MakeGenericType(t)))
                .ToDictionary(
                    t => t.Name,
                    t => new Func<PortfolioState>(() => (PortfolioState)Activator.CreateInstance(t)!));

        protected PortfolioState(PortfolioStatus status) =>
            Status = status;

        public PortfolioStatus Status { get; }

        public Holdings Holdings { get; protected init; } = Holdings.Empty;

        public virtual PortfolioState Open() =>
            throw new InvalidOperationException();

        public virtual PortfolioState SetShares(ShareCount shares) =>
            throw new InvalidOperationException();

        public virtual PortfolioState Close() =>
            throw new InvalidOperationException();

        public static PortfolioState FromSnapshot(PortfolioSnapshot snapshot)
        {
            var status = PortfolioStatus.Parse(snapshot.Status);
            return FromStatus(status) with
            {
                Holdings = new Holdings(
                    snapshot.Holdings.Select(
                        pair => new ShareCount(pair.Value, new Symbol(pair.Key)))),
            };
        }

        public PortfolioSnapshot ToSnapshot()
        {
            return new PortfolioSnapshot(Status)
            {
                Holdings = Holdings.ToDictionary(
                    shares => shares.Symbol.ToString(),
                    shares => (int)shares),
            };
        }

        private static PortfolioState FromStatus(PortfolioStatus status) =>
            Factories[status].Invoke();
    }

    internal abstract record PortfolioState<TSelf> : PortfolioState
        where TSelf : PortfolioState<TSelf>, new()
    {
        protected PortfolioState()
            : base(PortfolioStatus.For<TSelf>())
        {
        }
    }
}
