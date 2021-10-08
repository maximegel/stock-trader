using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Testing.Api;
using Xunit;

namespace StockTrader.Portfolios.Api.IntegrationTests.Endpoints
{
    public class ClosePortfolioEndpointTests : IClassFixture<PortfoliosApiFactory>
    {
        private readonly ICommandTestBed<IPortfolio> _testBed;

        public ClosePortfolioEndpointTests(PortfoliosApiFactory apiFactory) =>
            _testBed = TestBed.Create
                .WithApiFactory(apiFactory)
                .ForCommandOf<IPortfolio>();

        [Fact]
        public async Task ClosePortfolio_WhenOpened_Closes()
        {
            // Arrange
            var id = PortfolioId.Generate();
            await _testBed.Save(
                PortfolioFactory.LoadFromHistory(
                    id,
                    new PortfolioOpened("Main")));

            // Act
            var response = await _testBed.Client.PutAsync(
                $"/api/portfolio/{id}/close",
                null!);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            _testBed.CommittedEvents.Should().BeEquivalentTo(
                new[] { new PortfolioClosed() },
                opt => opt.ComparingRecordsByValue());
        }
    }
}
