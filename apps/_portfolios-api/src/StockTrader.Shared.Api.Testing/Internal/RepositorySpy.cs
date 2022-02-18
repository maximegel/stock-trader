using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Domain;

namespace StockTrader.Testing.Api.Internal
{
    public class RepositorySpy<TAggregate> : IRepository<TAggregate>
        where TAggregate : IAggregateRoot
    {
        private readonly List<IDomainEvent> _committedEvents = new();
        private readonly IRepository<TAggregate> _spied;

        public RepositorySpy(IRepository<TAggregate> spied) =>
            _spied = spied;

        public IEnumerable<IDomainEvent> CommittedEvents => _committedEvents;

        public Task<TAggregate?> Find(IIdentifier id, CancellationToken cancellationToken = default) =>
            _spied.Find(id, cancellationToken);

        public async Task Save(TAggregate aggregate, CancellationToken cancellationToken = default)
        {
            _committedEvents.Clear();
            var uncommittedEvents = CopyUncommittedEvents(aggregate);

            await _spied.Save(aggregate, cancellationToken);

            if (EventsHaveBeenCommitted(aggregate))
            {
                _committedEvents.AddRange(uncommittedEvents);
            }
        }

        private static IEnumerable<IDomainEvent> CopyUncommittedEvents(TAggregate aggregate) =>
            GetUncommittedEvents(aggregate).ToArray();

        private static bool EventsHaveBeenCommitted(TAggregate aggregate) =>
            !GetUncommittedEvents(aggregate).Any();

        private static IEnumerable<IDomainEvent> GetUncommittedEvents(TAggregate aggregate) =>
            (aggregate as IEventSourced<TAggregate>)?.UncommittedEvents.AsDomainEvents() ??
            Enumerable.Empty<IDomainEvent>();
    }
}
