using System.Collections.Generic;

namespace SimpleCqrs.Common.Domain
{
    public interface ICommand { }

    public interface ICommand<TAggregate> : ICommand where TAggregate : IAggregateRoot<TAggregate>
    {
        IEnumerable<IDomainEvent<TAggregate>> Execute(TAggregate aggregate);
    }
}