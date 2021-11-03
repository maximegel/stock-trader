using System;
using StockTrader.Portfolios.Domain.Payloads;

namespace StockTrader.Portfolios.Persistence.Internal
{
    internal record OrderRecord
    {
        public Guid Id { get; init; }

        public Guid PortfolioId { get; init; }

        public string Symbol { get; init; } = null!;

        public int Shares { get; init; }

        public TradeType TradeType { get; init; }

        public OrderType OrderType { get; init; }

        public decimal? PriceLimit { get; init; }

        public PortfolioRecord Portfolio { get; init; } = null!;
    }
}
