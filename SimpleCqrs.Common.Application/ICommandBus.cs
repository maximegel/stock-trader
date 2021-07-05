using System.Threading;
using System.Threading.Tasks;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Common.Application
{
    public interface ICommandBus
    {
        Task Send(ICommand command, CancellationToken cancellationToken = default);
    }
}