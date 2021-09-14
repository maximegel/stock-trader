using System;
using SimpleCqrs.Portfolios.Domain.Internal;

namespace SimpleCqrs.Portfolios.Persistence.DataModels
{
    internal class HoldingData
    {
        public Guid PortfolioId { get; init; }
        public int ShareCount { get; set; }
        public string Symbol { get; init; } = null!;

        public PortfolioData Portfolio { get; init; } = null!;
    }
}