using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Domain.Events
{
    public record FrozenEvent : IDomainEvent<BankAccount>
    {
        public BankAccount Apply(BankAccount aggregate) => aggregate;
    }
}