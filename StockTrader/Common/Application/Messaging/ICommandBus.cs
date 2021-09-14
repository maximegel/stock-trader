using System.Threading;
using System.Threading.Tasks;
using StockTrader.Common.Domain;

namespace StockTrader.Common.Application.Messaging
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;
    }
}