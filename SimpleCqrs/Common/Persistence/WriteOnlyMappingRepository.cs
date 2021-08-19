using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SimpleCqrs.Common.Application;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Persistence
{
    public class WriteOnlyMappingRepository<TSrc, TDest> : IWriteOnlyRepository<TSrc>
        where TSrc : IEntity
        where TDest : IEntity
    {
        private readonly IWriteOnlyRepository<TDest> _inner;
        private readonly IMapper _mapper;

        public WriteOnlyMappingRepository(IWriteOnlyRepository<TDest> inner, IMapper mapper)
        {
            _inner = inner;
            _mapper = mapper;
        }

        public async Task<TSrc?> Find(Identifier id, CancellationToken cancellationToken = default)
        {
            var dest = await _inner.Find(id, cancellationToken);
            var src = _mapper.Map<TSrc>(dest);
            return src;
        }

        public async Task Save(TSrc entity, CancellationToken cancellationToken = default)
        {
            var dest = _mapper.Map<TDest>(entity);
            var destFound = await _inner.Find(dest.Id, cancellationToken);
            if (destFound is null) await _inner.Save(dest, cancellationToken);
            else await _inner.Save(_mapper.Map(entity, destFound), cancellationToken);
        }
    }
}