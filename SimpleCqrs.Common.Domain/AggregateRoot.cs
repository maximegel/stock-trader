using System.Collections.Generic;
using System.Linq;

namespace SimpleCqrs.Common.Domain
{
    public abstract class AggregateRoot<TSelf, TId> : Entity<TId>, IAggregateRoot<TSelf>
        where TSelf : AggregateRoot<TSelf, TId>
    {
        private readonly List<IDomainEvent<TSelf>> _uncommittedEvents = new();

        protected AggregateRoot(TId id) : base(id) { }

        public IEnumerable<IDomainEvent<TSelf>> UncommittedEvents => _uncommittedEvents;

        public TSelf Apply(IEnumerable<IDomainEvent<TSelf>> events) => Apply(events.ToArray());

        public TSelf Execute(ICommand<TSelf> command)
        {
            var events = command.Execute((TSelf) this);
            var eventArray = events as IDomainEvent<TSelf>[] ?? events.ToArray();
            _uncommittedEvents.AddRange(eventArray);
            return Apply(eventArray);
        }

        public TSelf Apply(params IDomainEvent<TSelf>[] events)
        {
            return events.Aggregate((TSelf) this, (agg, e) => e.Apply(agg));
        }
    }
}