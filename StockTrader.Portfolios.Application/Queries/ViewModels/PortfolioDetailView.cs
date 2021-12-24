using System;

namespace StockTrader.Portfolios.Projection.PortfolioDetails
{
    public record PortfolioDetailView
    {
        public Guid Id { get; init; }

        public string? Name { get; set; }
    }
}
