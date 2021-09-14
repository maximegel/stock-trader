using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application.Messaging
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;
    }
}