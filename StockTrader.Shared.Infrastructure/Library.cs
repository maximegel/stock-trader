using System.Reflection;

namespace StockTrader.Shared.Infrastructure
{
    public static class Library
    {
        public static Assembly Assembly { get; } = typeof(Library).Assembly;
    }
}
