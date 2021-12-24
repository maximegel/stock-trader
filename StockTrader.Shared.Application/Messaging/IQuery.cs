using MediatR;

namespace StockTrader.Shared.Application.Messaging
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
