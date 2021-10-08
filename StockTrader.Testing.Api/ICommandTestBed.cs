using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using StockTrader.Shared.Domain;

namespace StockTrader.Testing.Api
{
    public interface ICommandTestBed<in TAggregate>
        where TAggregate : IAggregateRoot
    {
        HttpClient Client { get; }

        IEnumerable<IDomainEvent> CommittedEvents { get; }

        Task Save(TAggregate aggregate);
    }
}
