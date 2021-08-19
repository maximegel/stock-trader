using System;
using SimpleCqrs.Common.Domain;
using SimpleCqrs.Portfolios.Domain;

namespace SimpleCqrs.Portfolios.Persistence.DataModels
{
    internal class PortfolioData : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;

        Identifier IEntity.Id => PortfolioId.Parse(Id.ToString());
    }
}