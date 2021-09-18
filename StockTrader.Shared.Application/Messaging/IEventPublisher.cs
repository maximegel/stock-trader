using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockTrader.Shared.Application.Messaging
{
    public interface IEventPublisher
    {
        Task Publish(
            IEnumerable<IntegrationEvent> events, 
            CancellationToken cancellationToken = default);
    }
}