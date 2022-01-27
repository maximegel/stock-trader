using System.Reflection;

namespace StockTrader.Portfolios.Projection.Sql
{
    public static class Library
    {
        public static Assembly Assembly { get; } = typeof(Library).Assembly;
    }
}
