using FluentAssertions;
using StockTrader.Portfolios.Domain.Internal;
using StockTrader.Portfolios.Domain.Internal.States;
using Xunit;

namespace StockTrader.Portfolios.Domain.UnitTests.Internal
{
    public class PortfolioStateTests
    {
        [Fact]
        public void FromSnapshot_WithOpenedStatus_CreatesOpenedState()
        {
            // Arrange
            var snapshot = new PortfolioSnapshot(PortfolioStatus.For<Opened>());

            // Act
            var state = PortfolioState.FromSnapshot(snapshot);

            // Assert
            state.Should().BeOfType<Opened>();
        }
    }
}
