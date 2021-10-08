using System.Threading;
using System.Threading.Tasks;
using StockTrader.Shared.Domain;

namespace StockTrader.Shared.Application.Messaging
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;
    }
}
