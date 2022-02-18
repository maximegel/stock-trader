using System;
using System.Collections.Generic;

namespace StockTrader.Portfolios.Persistence.Internal
{
    internal record PortfolioModel
    {
        public Guid Id { get; init; }

        public string? Name { get; set; }

        public string Status { get; set; } = null!;

        public ICollection<HoldingModel> Holdings { get; init; } = new List<HoldingModel>();

        public ICollection<OrderModel> Orders { get; init; } = new List<OrderModel>();
    }
}
