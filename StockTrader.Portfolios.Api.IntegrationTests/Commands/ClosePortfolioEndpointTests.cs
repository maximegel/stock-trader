using System.Threading.Tasks;
using FluentAssertions;
using StockTrader.Api;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Shared.Api.Testing;
using Xunit;
using Xunit.Abstractions;

namespace StockTrader.Portfolios.Api.IntegrationTests.Commands
{
    public class ClosePortfolioEndpointTests : CommandEndpointTests<Startup, IPortfolio>
    {
        public ClosePortfolioEndpointTests(TestHostFactory factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task ClosePortfolio_WhenOpened_Closes()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(
                id,
                new PortfolioOpened("Main"));
            await Given(portfolio);

            // Act
            var response = await When(c => c.PutAsync(
                $"/api/portfolio/{id}/close",
                null!));

            // Assert
            ThenCommittedEvents(response, events =>
            {
                events.Should().BeEquivalentTo(
                    new[] { new PortfolioClosed() },
                    opt => opt.ComparingRecordsByValue());
            });
        }
    }
}
