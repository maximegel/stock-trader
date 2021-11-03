using FluentAssertions;
using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Shared.Domain;
using Xunit;

namespace StockTrader.Portfolios.Domain.UnitTests.Commands
{
    public class ClosePortfolioTests
    {
        [Fact]
        public void ClosePortfolio_WhenClosed_DoesNothing()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(
                id,
                new PortfolioOpened("Main"),
                new PortfolioClosed());

            // Act
            portfolio.Execute(new ClosePortfolio(id));

            // Assert
            portfolio.UncommittedEvents.Should().BeEmpty();
        }

        [Fact]
        public void ClosePortfolio_WhenOpened_Closes()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(
                id,
                new PortfolioOpened("Main"));

            // Act
            portfolio.Execute(new ClosePortfolio(id));

            // Assert
            portfolio.UncommittedEvents.AsDomainEvents().Should().BeEquivalentTo(
                new[] { new PortfolioClosed() },
                opt => opt.ComparingRecordsByValue());
        }
    }
}
