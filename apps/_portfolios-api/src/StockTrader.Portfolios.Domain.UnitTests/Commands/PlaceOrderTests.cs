using FluentAssertions;
using StockTrader.Portfolios.Domain.Commands;
using StockTrader.Portfolios.Domain.Events;
using StockTrader.Portfolios.Domain.Failures;
using StockTrader.Portfolios.Domain.Payloads;
using StockTrader.Shared.Domain;
using Xunit;

namespace StockTrader.Portfolios.Domain.UnitTests.Commands
{
    public class PlaceOrderTests
    {
        [Fact]
        public void PlaceOrder_WhenPortfolioClosed_Fails()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(
                id,
                new PortfolioOpened("Main"),
                new PortfolioClosed());

            // Act
            portfolio.Execute(new PlaceOrder(id, new OrderDetails(
                TradeType.Buy, 150, "TSLA", OrderType.Market)));

            // Assert
            portfolio.UncommittedEvents.AsDomainEvents().Should().BeEquivalentTo(
                new[] { new PortfolioCantBeClosed() });
        }

        [Fact]
        public void PlaceOrder_WhenPortfolioOpened_PlacesOrder()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(
                id,
                new PortfolioOpened("Main"));

            // Act
            portfolio.Execute(new PlaceOrder(id, new OrderDetails(
                TradeType.Buy, 150, "TSLA", OrderType.Market)));

            // Assert
            portfolio.UncommittedEvents.AsDomainEvents().Should().BeEquivalentTo(
                new[]
                {
                    new OrderPlaced(string.Empty, new OrderDetails(
                        TradeType.Buy, 150, "TSLA", OrderType.Market)),
                },
                opt => opt.Excluding(e => e.OrderId));
        }

        [Fact]
        public void PlaceSellOrder_WhenInsufficientShares_Fails()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(
                id,
                new PortfolioOpened("Main"));

            // Act
            portfolio.Execute(new PlaceOrder(id, new OrderDetails(
                TradeType.Sell, 10, "TSLA", OrderType.Market)));

            // Assert
            portfolio.UncommittedEvents.AsDomainEvents().Should().BeEquivalentTo(
                new[] { new InsufficientShares(0, 10, "TSLA") });
        }

        [Fact]
        public void PlaceSellOrder_WhenSufficientShares_DebitsShares()
        {
            // Arrange
            var id = PortfolioId.Generate();
            var portfolio = PortfolioFactory.LoadFromHistory(
                id,
                new PortfolioOpened("Main"),
                new SharesCredited(50, 50, "TSLA"));

            // Act
            portfolio.Execute(new PlaceOrder(id, new OrderDetails(
                TradeType.Sell, 10, "TSLA", OrderType.Market)));

            // Assert
            portfolio.UncommittedEvents.AsDomainEvents().Should().SatisfyRespectively(
                e => e.Should().BeEquivalentTo(new SharesDebited(10, 40, "TSLA")),
                e => e.Should().BeEquivalentTo(
                    new OrderPlaced(string.Empty, new OrderDetails(
                        TradeType.Sell, 10, "TSLA", OrderType.Market)),
                    opt => opt.Excluding(o => o.OrderId)));
        }
    }
}
