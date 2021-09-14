using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockTrader.Common.Application.Persistence;
using StockTrader.Common.Domain;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Persistence.DataModels;

namespace StockTrader.Portfolios.Persistence
{
    internal class PortfolioDbRepository : IRepository<IPortfolio>
    {
        private readonly PortfolioDbContext _context;

        public PortfolioDbRepository(PortfolioDbContext context) =>
            _context = context;

        public async Task<IPortfolio?> Find(Identifier id, CancellationToken cancellationToken = default)
        {
            var data = await _context.Portfolios
                .Include(p => p.Holdings)
                .Include(p => p.Orders)
                .FirstOrDefaultAsync(p => p.Id.ToString() == id.ToString(), cancellationToken);

            if (data == null) return null;

            var snapshot = new PortfolioSnapshot((PortfolioId) id)
            {
                Status = data.Status,
                Holdings = data.Holdings.ToDictionary(h => h.Symbol, h => h.ShareCount)
            };
            var aggregate = PortfolioFactory.Load((PortfolioId) id);
            return aggregate.RestoreSnapshot(snapshot);
        }

        public async Task Save(IPortfolio aggregate, CancellationToken cancellationToken = default)
        {
            var id = (Guid) aggregate.Id;
            var data = await _context.Portfolios.FindAsync(new object[] {id}, cancellationToken)
                       ?? _context.Portfolios.Add(new PortfolioData {Id = id}).Entity;
            
            var snapshot = aggregate.TakeSnapshot();
            ApplyChanges(data, snapshot);
            
            var events = aggregate.UncommittedEvents;
            ApplyChanges(data, events);

            _context.Portfolios.Update(data);
            
            await _context.SaveChangesAsync(cancellationToken);
            events.MarkAsCommitted();
        }

        private static void ApplyChanges(PortfolioData data, PortfolioSnapshot snapshot)
        {
            data.Status = snapshot.Status;
        }

        private static void ApplyChanges(PortfolioData data, IEnumerable<PortfolioEvent> events) =>
            events.ToList().ForEach(domainEvent => ApplyChanges(data, domainEvent));

        private static void ApplyChanges(PortfolioData data, PortfolioEvent domainEvent)
        {
            switch (domainEvent)
            {
                case PortfolioOpened (var name):
                    data.Name = name;
                    break;
                case SharesDebited (var symbol, var shares):
                    var holding = data.Holdings.Single(h => h.Symbol == symbol);
                    holding.ShareCount -= shares;
                    break;
                case OrderPlaced (var orderId, var details):
                    data.Orders.Add(new OrderData
                    {
                        Id = Guid.Parse(orderId),
                        Symbol = details.Symbol,
                        Shares = details.Shares,
                        TradeType = details.TradeType,
                        OrderType = details.OrderType,
                        PriceLimit = details.PriceLimit
                    });
                    break;
                default: return;
            }
        }
    }
}