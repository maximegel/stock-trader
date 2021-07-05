using System.Threading;
using System.Threading.Tasks;

namespace SimpleCqrs.Common.Domain
{
    public interface IWriteOnlyRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task<TAggregate> Find(string id, CancellationToken cancellationToken = default);

        Task Save(TAggregate aggregate, CancellationToken cancellationToken = default);
    }
}