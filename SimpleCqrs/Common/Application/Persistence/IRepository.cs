using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application.Persistence
{
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task<TAggregate?> Find(Identifier id, CancellationToken cancellationToken = default);

        Task Save(TAggregate aggregate, CancellationToken cancellationToken = default);
    }
}