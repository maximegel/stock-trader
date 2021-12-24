using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StockTrader.Api;
using StockTrader.Shared.Application.Messaging;
using Xunit;
using Xunit.Abstractions;

namespace StockTrader.Portfolios.Api.IntegrationTests
{
    [Collection("TestHost")]
    public abstract class QueryEndpointTests : EndpointTests
    {
        protected QueryEndpointTests(
            WebApplicationFactory<Startup> factory,
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
