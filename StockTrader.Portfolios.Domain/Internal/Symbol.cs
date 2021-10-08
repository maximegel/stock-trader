using System;
using System.Text.RegularExpressions;
using StockTrader.Shared.Domain;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal sealed class Symbol : UnaryValueObject<Symbol, string>
    {
        private static readonly Regex Pattern =
            new(@"^[A-Z]{1,5}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public Symbol(string value)
            : base(value)
        {
            if (!Pattern.IsMatch(value))
            {
                throw new ArgumentException("Symbol must be an alpha string of 1 to 5 characters.");
            }
        }
    }
}
