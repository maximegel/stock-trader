using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockTrader.Shared.Application.Messaging
{
    public interface IEventPublisher<in TEvent>
        where TEvent : class
    {
        Task Publish(
            IEnumerable<TEvent> events,
            CancellationToken cancellationToken = default);
    }
}
