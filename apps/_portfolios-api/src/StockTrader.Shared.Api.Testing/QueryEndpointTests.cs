using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StockTrader.Shared.Application.Messaging;
using Xunit.Abstractions;

namespace StockTrader.Shared.Api.Testing
{
    public abstract class QueryEndpointTests<TEntryPoint> : EndpointTests<TEntryPoint>
        where TEntryPoint : class
    {
        protected QueryEndpointTests(
            WebApplicationFactory<TEntryPoint> factory,
            ITestOutputHelper output)
            : base(factory, output)
        {
        }

        protected static async Task ThenResult<TResult>(
            HttpResponseMessage response,
            Action<TResult> assertions)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = (await response.Content.ReadFromJsonAsync<TResult>())!;
            assertions(result);
        }

        protected async Task Given(IEnumerable<IIntegrationEvent> events)
        {
            var publisher = Services.GetRequiredService<IEventPublisher>();
            await publisher.Publish(events);
        }
    }
}
