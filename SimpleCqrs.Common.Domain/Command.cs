using System.Collections.Generic;

namespace SimpleCqrs.Common.Domain
{
    public abstract record Command<TAggregate>(string AggregateId) : ICommand<TAggregate>
        where TAggregate : IAggregateRoot<TAggregate>
    {
        public abstract IEnumerable<IDomainEvent<TAggregate>> Execute(TAggregate aggregate);
    }
}