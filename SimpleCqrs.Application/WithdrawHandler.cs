using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Application;
using SimpleCqrs.Common.Domain;
using SimpleCqrs.Domain;
using SimpleCqrs.Domain.Commands;

namespace SimpleCqrs.Application
{
    public class WithdrawHandler : CommandHandler<WithdrawCommand, BankAccount>
    {
        private readonly IWriteOnlyRepository<BankAccount> _repository;

        public WithdrawHandler(IWriteOnlyRepository<BankAccount> repository) => _repository = repository;

        protected override async Task Handle(WithdrawCommand command, CancellationToken cancellationToken)
        {
            var aggregate = await _repository.Find(command.AggregateId, cancellationToken);
            aggregate.Execute(command);
            await _repository.Save(aggregate, cancellationToken);
        }
    }
}