using System;
using System.Text.RegularExpressions;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Portfolios.Domain.Internal
{
    internal sealed class Symbol : UnaryValueObject<string>
    {
        private static readonly Regex Pattern =
            new(@"^[A-Z]{1,5}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly string _value;

        public Symbol(string value)
        {
            if (!Pattern.IsMatch(value))
                throw new ArgumentException("Symbol must be an alpha string of 1 to 5 characters.");
            _value = value;
        }

        protected override string GetValue() => _value;
    }
}