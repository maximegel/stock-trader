using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Persistence.Internal;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Persistence
{
    public class PortfolioDbRepository : IRepository<IPortfolio>
    {
        private readonly PortfoliosWriteDbContext _context;

        public PortfolioDbRepository(PortfoliosWriteDbContext context) =>
            _context = context;

        public async Task<IPortfolio?> Find(IIdentifier id, CancellationToken cancellationToken = default)
        {
            var rec = await _context.Portfolios
                .Include(p => p.Holdings)
                .Include(p => p.Orders)
                .FirstOrDefaultAsync(p => p.Id.ToString() == id.ToString(), cancellationToken);

            if (rec == null)
            {
                return null;
            }

            return PortfolioFactory.LoadFromSnapshot((PortfolioId)id, new PortfolioSnapshot(rec.Status)
            {
                Holdings = rec.Holdings.ToDictionary(h => h.Symbol, h => h.ShareCount),
            });
        }

        public async Task Save(IPortfolio aggregate, CancellationToken cancellationToken = default)
        {
            var id = (Guid)aggregate.Id;
            var rec = await _context.Portfolios.FindAsync(new object[] { id }, cancellationToken);
            if (rec == null)
            {
                var newData = new PortfolioRecord { Id = id };
                ApplyChanges(newData, aggregate);
                _context.Portfolios.Add(newData);
            }
            else
            {
                ApplyChanges(rec, aggregate);
                _context.Portfolios.Update(rec);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private static void ApplyChanges(PortfolioRecord rec, IPortfolio aggregate)
        {
            var snapshot = aggregate.TakeSnapshot();
            ApplyChanges(rec, snapshot);

            var events = aggregate.UncommittedEvents;
            ApplyChanges(rec, events);
        }

        private static void ApplyChanges(PortfolioRecord rec, PortfolioSnapshot snapshot)
        {
            rec.Status = snapshot.Status;
        }

        private static void ApplyChanges(PortfolioRecord rec, IEnumerable events) =>
            events
                .OfType<PortfolioEvent>()
                .ToList()
                .ForEach(domainEvent => ApplyChanges(rec, domainEvent));

        private static void ApplyChanges(PortfolioRecord rec, PortfolioEvent domainEvent)
        {
            switch (domainEvent)
            {
                case PortfolioOpened(var name):
                    rec.Name = name;
                    break;
                case SharesDebited(_, var remainingShares, var symbol):
                    var holding = rec.Holdings.Single(h => h.Symbol == symbol);
                    holding.ShareCount = remainingShares;
                    break;
                case OrderPlaced(var orderId, var details):
                    rec.Orders.Add(new OrderRecord
                    {
                        Id = Guid.Parse(orderId),
                        Symbol = details.Symbol,
                        Shares = details.Shares,
                        TradeType = details.TradeType,
                        OrderType = details.OrderType,
                        PriceLimit = details.PriceLimit,
                    });
                    break;
                default: return;
            }
        }
    }
}
