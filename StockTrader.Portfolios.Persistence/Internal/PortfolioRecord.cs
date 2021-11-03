using System;
using System.Collections.Generic;

namespace StockTrader.Portfolios.Persistence.Internal
{
    internal record PortfolioRecord
    {
        public Guid Id { get; init; }

        public string? Name { get; set; }

        public string Status { get; set; } = null!;

        public ICollection<HoldingRecord> Holdings { get; init; } = new List<HoldingRecord>();

        public ICollection<OrderRecord> Orders { get; init; } = new List<OrderRecord>();
    }
}
