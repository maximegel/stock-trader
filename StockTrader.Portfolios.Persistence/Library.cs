using System.Reflection;

namespace StockTrader.Portfolios.Persistence
{
    public static class Library
    {
        public static Assembly Assembly { get; } = typeof(Library).Assembly;
    }
}
