using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace StockTrader.Shared.Application.Messaging
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IIntegrationEvent
    {
    }
}
