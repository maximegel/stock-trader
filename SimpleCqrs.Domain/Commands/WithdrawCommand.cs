using System.Collections.Generic;
using SimpleCqrs.Common.Domain;
using SimpleCqrs.Domain.Events;

namespace SimpleCqrs.Domain.Commands
{
    public record WithdrawCommand(string AggregateId, decimal Amount) : Command<BankAccount>(AggregateId)
    {
        public override IEnumerable<IDomainEvent<BankAccount>> Execute(BankAccount aggregate)
        {
            if (aggregate.Balance - new MoneyAmount(Amount) >= new MoneyAmount(0))
                yield return new WithdrawnEvent(Amount);
        }
    }
}