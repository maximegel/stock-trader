using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application
{
    public interface IWriteOnlyRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity?> Find(Identifier id, CancellationToken cancellationToken = default);

        Task Save(TEntity entity, CancellationToken cancellationToken = default);
    }
}