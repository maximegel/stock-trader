using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Domain.Events
{
    public record OpenedEvend : IDomainEvent<BankAccount>
    {
        public BankAccount Apply(BankAccount aggregate) => aggregate;
    }
}