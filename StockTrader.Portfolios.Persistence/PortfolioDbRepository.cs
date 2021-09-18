using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Persistence.DataModels;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Persistence
{
    public class PortfolioDbRepository : IRepository<IPortfolio>
    {
        private readonly PortfolioDbContext _context;

        public PortfolioDbRepository(PortfolioDbContext context) =>
            _context = context;

        public async Task<IPortfolio?> Find(IIdentifier id, CancellationToken cancellationToken = default)
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
            var data = await _context.Portfolios.FindAsync(new object[] { id }, cancellationToken);
            if (data == null)
            {
                var newData = new PortfolioData { Id = id };
                ApplyChanges(newData, aggregate);
                _context.Portfolios.Add(newData);
            }
            else
            {
                ApplyChanges(data, aggregate);
                _context.Portfolios.Update(data);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        private static void ApplyChanges(PortfolioData data, IPortfolio aggregate)
        {
            var snapshot = aggregate.TakeSnapshot();
            ApplyChanges(data, snapshot);
            
            var events = aggregate.UncommittedEvents;
            ApplyChanges(data, events); 
        }

        private static void ApplyChanges(PortfolioData data, PortfolioSnapshot snapshot)
        {
            data.Status = snapshot.Status;
        }

        private static void ApplyChanges(PortfolioData data, IEnumerable events) =>
            events
                .OfType<PortfolioEvent>()
                .ToList()
                .ForEach(domainEvent => ApplyChanges(data, domainEvent));

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