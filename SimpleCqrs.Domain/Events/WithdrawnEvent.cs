using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Domain.Events
{
    public record WithdrawnEvent(decimal Amount) : IDomainEvent<BankAccount>
    {
        public BankAccount Apply(BankAccount aggregate)
        {
            aggregate.Balance -= new MoneyAmount(Amount);
            return aggregate;
        }
    }
}