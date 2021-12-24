using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using StockTrader.Portfolios.Api.Commands;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Testing.Api;
using Xunit;

namespace StockTrader.Portfolios.Api.IntegrationTests.Commands
{
    public class OpenPortfolioEndpointTests : IClassFixture<TestHostFactory>
    {
        private readonly ICommandTestBed<IPortfolio> _testBed;

        public OpenPortfolioEndpointTests(TestHostFactory apiFactory) =>
            _testBed = TestBed.Create
                .WithApiFactory(apiFactory)
                .ForCommandOf<IPortfolio>();

        [Fact]
        public async Task OpenPortfolio_WhenNotOpened_Opens()
        {
            // Act
            var response = await _testBed.Client.PostAsJsonAsync(
                "/api/portfolio/open",
                new OpenPortfolioDto("Main"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            _testBed.CommittedEvents.Should().BeEquivalentTo(
                new[] { new PortfolioOpened("Main") });
        }
    }
}
