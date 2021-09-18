using System.Threading;
using System.Threading.Tasks;
using StockTrader.Shared.Domain;

namespace StockTrader.Shared.Application.Persistence
{
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task<TAggregate?> Find(IIdentifier id, CancellationToken cancellationToken = default);

        Task Save(TAggregate aggregate, CancellationToken cancellationToken = default);
    }
}