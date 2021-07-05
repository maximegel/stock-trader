using System.Collections.Generic;
using SimpleCqrs.Common.Domain;
using SimpleCqrs.Domain.Events;

namespace SimpleCqrs.Domain.Commands
{
    public record FreezeCommand(string AggregateId) : Command<BankAccount>(AggregateId)
    {
        public override IEnumerable<IDomainEvent<BankAccount>> Execute(BankAccount aggregate)
        {
            yield return new FrozenEvent();
        }
    }
}