using System;
using StockTrader.Portfolios.Domain.Payloads;

namespace StockTrader.Portfolios.Persistence.DataModels
{
    internal class OrderData
    {
        public Guid Id { get; init; }

        public Guid PortfolioId { get; init; }

        public string Symbol { get; init; } = null!;

        public int Shares { get; init; }

        public TradeType TradeType { get; init; }

        public OrderType OrderType { get; init; }

        public decimal? PriceLimit { get; init; }

        public PortfolioData Portfolio { get; init; } = null!;
    }
}
