using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command;
    }
}