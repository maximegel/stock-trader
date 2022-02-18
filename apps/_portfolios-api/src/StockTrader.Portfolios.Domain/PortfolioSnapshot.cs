using System.Collections.Generic;

namespace StockTrader.Portfolios.Domain
{
    public record PortfolioSnapshot(string Status)
    {
        public IReadOnlyDictionary<string, int> Holdings { get; init; } = new Dictionary<string, int>();
    }
}
