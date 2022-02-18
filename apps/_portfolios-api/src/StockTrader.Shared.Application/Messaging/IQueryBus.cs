using System.Threading;
using System.Threading.Tasks;

namespace StockTrader.Shared.Application.Messaging
{
    public interface IQueryBus
    {
        Task<TResult> Send<TResult>(
            IQuery<TResult> query,
            CancellationToken cancellationToken = default);
    }
}
