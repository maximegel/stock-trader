using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal class PortfolioStatus : UnaryValueObject<PortfolioStatus, string>
    {
        private PortfolioStatus(string value)
            : base(value)
        {
        }

        public static PortfolioStatus For<TState>()
            where TState : PortfolioState
        {
            return new PortfolioStatus(typeof(TState).Name);
        }

        public static PortfolioStatus Parse(string input) =>
            new(input);

        public bool Is<TState>()
            where TState : PortfolioState
        {
            return Equals(For<TState>());
        }
    }
}
