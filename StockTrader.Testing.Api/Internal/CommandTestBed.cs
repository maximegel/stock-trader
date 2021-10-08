using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Domain;

namespace StockTrader.Testing.Api.Internal
{
    internal class CommandTestBed<TEntryPoint, TAggregate> : ICommandTestBed<TAggregate>
        where TEntryPoint : class
        where TAggregate : IAggregateRoot
    {
        private readonly WebApplicationFactory<TEntryPoint> _factory;
        private RepositorySpy<TAggregate>? _repositorySpy;

        public CommandTestBed(WebApplicationFactory<TEntryPoint> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.Decorate<IRepository<TAggregate>>(spied =>
                            _repositorySpy = new RepositorySpy<TAggregate>(spied));
                    }));
            Client = _factory.CreateClient();
        }

        public HttpClient Client { get; }

        public IEnumerable<IDomainEvent> CommittedEvents =>
            _repositorySpy?.CommittedEvents
            ?? Enumerable.Empty<IDomainEvent>();

        public async Task Save(TAggregate aggregate)
        {
            using var scope = _factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var repository = scopedServices.GetRequiredService<IRepository<TAggregate>>();
            await repository.Save(aggregate);
        }
    }
}
