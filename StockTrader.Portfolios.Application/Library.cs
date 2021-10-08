using System.Reflection;

namespace StockTrader.Portfolios.Application
{
    public static class Library
    {
        public static Assembly Assembly { get; } = typeof(Library).Assembly;
    }
}
