using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using StockTrader.Api;
using StockTrader.Portfolios.Api.Commands;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Shared.Api.Testing;
using Xunit;
using Xunit.Abstractions;

namespace StockTrader.Portfolios.Api.IntegrationTests.Commands
{
    public class OpenPortfolioEndpointTests : CommandEndpointTests<Startup, IPortfolio>
    {
        public OpenPortfolioEndpointTests(TestHostFactory factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task OpenPortfolio_WhenNotOpened_Opens()
        {
            // Act
            var response = await When(c => c.PostAsJsonAsync(
                "/api/portfolio/open",
                new OpenPortfolioDto("Main")));

            // Assert
            ThenCommittedEvents(response, events =>
            {
                events.Should().BeEquivalentTo(
                    new[] { new PortfolioOpened("Main") });
            });
        }
    }
}
