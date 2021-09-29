using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using StockTrader.Portfolios.Api.Endpoints;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Payloads;
using StockTrader.Testing.Api;
using Xunit;

namespace StockTrader.Portfolios.Api.IntegrationTests.Endpoints
{
    public class PlaceOrderEndpointTests : IClassFixture<PortfoliosApiFactory>
    {
        private readonly ICommandTestBed<IPortfolio> _testBed;

        public PlaceOrderEndpointTests(PortfoliosApiFactory apiFactory) =>
            _testBed = TestBed.Create
                .WithApiFactory(apiFactory)
                .ForCommandOf<IPortfolio>();
        
        [Fact]
        public async Task Call_CommitsEvents()
        {
            // Arrange
            var id = PortfolioId.Generate();
            await _testBed.Save(PortfolioFactory.LoadFromHistory(id,
                new PortfolioOpened("Main")));

            // Act
            var response = await _testBed.Client.PutAsJsonAsync(
                $"/api/portfolio/{id}/place-order",
                new PlaceOrderDto(new OrderDetails(
                    TradeType.Buy, 150, "TSLA", OrderType.Market)));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            _testBed.CommittedEvents.Should().BeEquivalentTo(
                new[]
                {
                    new OrderPlaced("", new OrderDetails(
                        TradeType.Buy, 150, "TSLA", OrderType.Market))
                },
                opt => opt.WithStrictOrdering().Excluding(e => e.OrderId));
        }
    }
}