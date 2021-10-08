using System;

namespace StockTrader.Shared.Domain
{
    public abstract class Uid<TSelf> : Identifier<TSelf, long>
        where TSelf : Uid<TSelf>, new()
    {
        protected Uid()
            : base(0)
        {
        }

        protected Uid(long value)
            : base(value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Uid must be a positive number.", nameof(value));
            }
        }

        public static TSelf Default { get; } = new();

        public static implicit operator Uid<TSelf>(string value) =>
            Parse(value);

        private static TSelf Parse(string input) =>
            new() { Value = long.Parse(input) };
    }
}
