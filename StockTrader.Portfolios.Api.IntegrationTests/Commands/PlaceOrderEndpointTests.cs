using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using StockTrader.Api;
using StockTrader.Portfolios.Api.Commands;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Payloads;
using StockTrader.Shared.Api.Testing;
using Xunit;
using Xunit.Abstractions;

namespace StockTrader.Portfolios.Api.IntegrationTests.Commands
{
    public class PlaceOrderEndpointTests : CommandEndpointTests<Startup, IPortfolio>
    {
        public PlaceOrderEndpointTests(TestHostFactory factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task PlaceOrder_WhenPortfolioOpened_PlacesOrder()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(
                id,
                new PortfolioOpened("Main"));
            await Given(portfolio);

            // Act
            var response = await When(c => c.PutAsJsonAsync(
                $"/api/portfolio/{id}/place-order",
                new PlaceOrderDto(new OrderDetails(
                    TradeType.Buy, 150, "TSLA", OrderType.Market))));

            // Assert
            ThenCommittedEvents(response, events =>
            {
                events.Should().BeEquivalentTo(
                    new[]
                    {
                        new OrderPlaced(string.Empty, new OrderDetails(
                            TradeType.Buy, 150, "TSLA", OrderType.Market)),
                    },
                    opt => opt.Excluding(e => e.OrderId));
            });
        }
    }
}
