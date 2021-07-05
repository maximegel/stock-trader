using System;
using System.Collections.Generic;
using SimpleCqrs.Common.Domain;
using SimpleCqrs.Domain.Events;

namespace SimpleCqrs.Domain.Commands
{
    public record OpenCommand() : Command<BankAccount>(Guid.NewGuid().ToString())
    {
        public override IEnumerable<IDomainEvent<BankAccount>> Execute(BankAccount aggregate)
        {
            yield return new OpenedEvend();
        }
    }
}