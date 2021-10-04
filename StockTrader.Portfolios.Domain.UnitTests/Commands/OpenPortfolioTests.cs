using FluentAssertions;
using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Failures;
using Xunit;

namespace StockTrader.Portfolios.Domain.UnitTests.Commands
{
    public class OpenPortfolioTests
    {
        [Fact]
        public void OpenPortfolio_WhenNotOpened_Opens()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(id);

            // Act
            portfolio.Execute(new OpenPortfolio("Main"));

            // Assert
            portfolio.UncommittedEvents.Should().BeEquivalentTo(
                new[] { new PortfolioOpened("Main") });
        }

        [Fact]
        public void OpenPortfolio_WhenOpened_Fails()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(id,
                new PortfolioOpened("Main"));

            // Act
            portfolio.Execute(new OpenPortfolio("Main"));

            // Assert
            portfolio.UncommittedEvents.Should().BeEquivalentTo(
                new[] { new PortfolioAlreadyOpened() },
                opt => opt.ComparingRecordsByValue());
        }
    }
}