using System.Collections.Generic;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain
{
    public class PortfolioSnapshot : Entity<PortfolioId>
    {
        public PortfolioSnapshot(PortfolioId id) : base(id) { }

        public IReadOnlyDictionary<string, int> Holdings { get; init; } = new Dictionary<string, int>();
        public PortfolioStatus Status { get; init; }
    }
}