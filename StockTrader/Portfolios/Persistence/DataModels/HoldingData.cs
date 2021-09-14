using System;
using StockTrader.Portfolios.Domain.Internal;

namespace StockTrader.Portfolios.Persistence.DataModels
{
    internal class HoldingData
    {
        public Guid PortfolioId { get; init; }
        public int ShareCount { get; set; }
        public string Symbol { get; init; } = null!;

        public PortfolioData Portfolio { get; init; } = null!;
    }
}