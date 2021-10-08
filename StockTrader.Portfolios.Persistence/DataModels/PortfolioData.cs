using System;
using System.Collections.Generic;

namespace StockTrader.Portfolios.Persistence.DataModels
{
    internal class PortfolioData
    {
        public Guid Id { get; init; }

        public string? Name { get; set; }

        public string Status { get; set; } = null!;

        public ICollection<HoldingData> Holdings { get; init; } = new List<HoldingData>();

        public ICollection<OrderData> Orders { get; init; } = new List<OrderData>();
    }
}
