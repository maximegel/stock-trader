using System.Collections.Generic;

namespace SimpleCqrs.Common.Domain
{
    public interface IAggregateRoot : IEntity { }

    public interface IAggregateRoot<TSelf> : IAggregateRoot where TSelf : IAggregateRoot<TSelf>
    {
        IEnumerable<IDomainEvent<TSelf>> UncommittedEvents { get; }

        TSelf Apply(IEnumerable<IDomainEvent<TSelf>> events);

        TSelf Execute(ICommand<TSelf> command);
    }
}