using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StockTrader.Shared.Application.Persistence;
using StockTrader.Shared.Domain;
using StockTrader.Testing.Api.Internal;
using Xunit.Abstractions;

namespace StockTrader.Shared.Api.Testing
{
    public abstract class CommandEndpointTests<TEntryPoint, TAggregate> :
        EndpointTests<TEntryPoint>
        where TEntryPoint : class
        where TAggregate : IAggregateRoot
    {
        private RepositorySpy<TAggregate>? _repositorySpy;

        protected CommandEndpointTests(
            WebApplicationFactory<TEntryPoint> factory,
            ITestOutputHelper output)
            : base(factory, output)
        {
        }

        private IEnumerable<IDomainEvent> CommittedEvents =>
            _repositorySpy?.CommittedEvents
            ?? Enumerable.Empty<IDomainEvent>();

        protected async Task Given(TAggregate aggregate)
        {
            var repository = Services.GetRequiredService<IRepository<TAggregate>>();
            await repository.Save(aggregate);
        }

        protected void ThenCommittedEvents(
            HttpResponseMessage response,
            Action<IEnumerable<IDomainEvent>> assertions)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            assertions(CommittedEvents);
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            services.Decorate<IRepository<TAggregate>>(spied =>
                _repositorySpy = new RepositorySpy<TAggregate>(spied));
        }
    }
}
