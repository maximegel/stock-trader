using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace StockTrader.Shared.Application.Messaging
{
    public abstract class QueryHandler<TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> IRequestHandler<TQuery, TResult>.Handle(
            TQuery request,
            CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }

        protected abstract Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
