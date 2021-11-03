using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Application.Messaging
{
    public interface IEventUpgrader<in TSource, out TDestination>
        where TSource : class
        where TDestination : class
    {
        TDestination Upgrade(TSource sourceEvent);

        IEnumerable<TDestination> Upgrade(IEnumerable<TSource> sourceEvents) =>
            sourceEvents.Select(Upgrade);
    }
}
