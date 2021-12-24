using System;

namespace StockTrader.Portfolios.Persistence.Internal
{
    internal record HoldingModel
    {
        public Guid PortfolioId { get; init; }

        public int ShareCount { get; set; }

        public string Symbol { get; init; } = null!;

        public PortfolioModel Portfolio { get; init; } = null!;
    }
}
