using System;

namespace StockTrader.Portfolios.Persistence.Internal
{
    internal record HoldingRecord
    {
        public Guid PortfolioId { get; init; }

        public int ShareCount { get; set; }

        public string Symbol { get; init; } = null!;

        public PortfolioRecord Portfolio { get; init; } = null!;
    }
}
