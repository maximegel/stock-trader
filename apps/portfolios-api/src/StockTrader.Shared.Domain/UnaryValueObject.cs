using System.Collections.Generic;

namespace StockTrader.Shared.Domain
{
    public abstract class UnaryValueObject<TSelf, TValue> : ValueObject<TSelf>
        where TSelf : UnaryValueObject<TSelf, TValue>
    {
        protected UnaryValueObject(TValue value) =>
            Value = value;

        protected TValue Value { get; init; }

        public static implicit operator TValue(UnaryValueObject<TSelf, TValue> self) =>
            self.Value;

        public override string ToString() =>
            Value?.ToString() ?? string.Empty;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            if (Value is not null)
            {
                yield return Value;
            }
        }
    }
}
