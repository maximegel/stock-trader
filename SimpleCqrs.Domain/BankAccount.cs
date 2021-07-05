using System;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Domain
{
    public class BankAccount : AggregateRoot<BankAccount, Guid>
    {
        public BankAccount(Guid id) : base(id) { }

        public MoneyAmount Balance { get; internal set; }
    }
}