using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Common.Application;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Persistence
{
    public class DbWriteOnlyRepository<TEntity> : IWriteOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbContext _context;

        public DbWriteOnlyRepository(DbContext context) => _context = context;

        public async Task<TEntity?> Find(Identifier id, CancellationToken cancellationToken = default)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            return await _context.Set<TEntity>().FindAsync(id, cancellationToken);
        }

        public async Task Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity.Id is null) throw new ArgumentException("Id cannot be null.", nameof(entity));
            var found = await Find(entity.Id, cancellationToken);
            if (found is null) _context.Add(entity);
            else _context.Update(found);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}