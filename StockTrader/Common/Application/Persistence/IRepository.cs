using System.Threading;
using System.Threading.Tasks;
using StockTrader.Common.Domain;

namespace StockTrader.Common.Application.Persistence
{
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task<TAggregate?> Find(Identifier id, CancellationToken cancellationToken = default);

        Task Save(TAggregate aggregate, CancellationToken cancellationToken = default);
    }
}