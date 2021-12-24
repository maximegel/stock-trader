using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using StockTrader.Portfolios.Application;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Projection.PortfolioDetails;
using Xunit;
using Xunit.Abstractions;

namespace StockTrader.Portfolios.Api.IntegrationTests.Queries
{
    public class GetPortfolioDetailEndpointTests : QueryEndpointTests
    {
        public GetPortfolioDetailEndpointTests(TestHostFactory factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task GetPortfolioDetail_AfterOpened_ReturnsSome()
        {
            // Arrange
            var id = PortfolioId.Generate();
            await Given(PortfolioIntegrationEvent.CreateRange(
                id,
                new PortfolioOpened("Main")));

            // Act
            var response = await When(c => c.GetAsync($"/api/portfolio/{id}"));

            // Assert
            await ThenResult<PortfolioDetailView>(response, result =>
            {
                result.Id.Should().Be(id.ToString());
                result.Name.Should().Be("Main");
            });
        }
    }
}
