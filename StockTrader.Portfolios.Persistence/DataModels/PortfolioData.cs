using System;
using System.Collections.Generic;
using StockTrader.Portfolios.Domain;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Persistence.DataModels
{
    internal class PortfolioData : Entity
    {
        public Guid Id { get; init; }
        public string? Name { get; set; }
        public PortfolioStatus Status { get; set; }
        public ICollection<HoldingData> Holdings { get; init; } = new List<HoldingData>();
        public ICollection<OrderData> Orders { get; init; } = new List<OrderData>();

        protected override Identifier GetId() => Id;
    }
}