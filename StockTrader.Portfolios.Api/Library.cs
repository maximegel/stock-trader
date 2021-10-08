using System.Reflection;

namespace StockTrader.Portfolios.Api
{
    public static class Library
    {
        public static Assembly Assembly { get; } = typeof(Library).Assembly;
    }
}
