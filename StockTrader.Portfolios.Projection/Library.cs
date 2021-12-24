using System.Reflection;

namespace StockTrader.Portfolios.Projection
{
    public static class Library
    {
        public static Assembly Assembly { get; } = typeof(Library).Assembly;
    }
}
