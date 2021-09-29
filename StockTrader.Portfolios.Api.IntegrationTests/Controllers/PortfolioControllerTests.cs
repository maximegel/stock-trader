using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using StockTrader.Portfolios.Api.Controllers;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Payloads;
using StockTrader.Testing.Api;
using Xunit;

namespace StockTrader.Portfolios.Api.IntegrationTests.Controllers
{
    public class PortfolioControllerTests : IClassFixture<PortfoliosApiFactory>
    {
        private readonly ICommandTestBed<IPortfolio> _testBed;

        public PortfolioControllerTests(PortfoliosApiFactory apiFactory) =>
            _testBed = TestBed.Create
                .WithApiFactory(apiFactory)
                .ForCommandOf<IPortfolio>();

        [Fact]
        public async Task Open_CommitsEvents()
        {
            // Act
            var response = await _testBed.Client.PostAsJsonAsync(
                "/api/portfolio/open",
                new OpenPortfolioDto("Main"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            _testBed.CommittedEvents.Should().BeEquivalentTo(
                new[] { new PortfolioOpened("Main") },
                opt => opt.WithStrictOrdering());
        }

        [Fact]
        public async Task PlaceOrder_CommitsEvents()
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

        [Fact]
        public async Task Close_CommitsEvents()
        {
            // Arrange
            var id = PortfolioId.Generate();
            await _testBed.Save(
                PortfolioFactory.LoadFromHistory(id,
                    new PortfolioOpened("Main")));

            // Act
            var response = await _testBed.Client.PutAsync(
                $"/api/portfolio/{id}/close",
                null);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            _testBed.CommittedEvents.Should().BeEquivalentTo(
                new[] { new PortfolioClosed() },
                opt => opt.WithStrictOrdering().ComparingRecordsByValue());
        }
    }
}