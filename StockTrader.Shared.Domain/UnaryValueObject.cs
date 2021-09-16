using System.Collections.Generic;

namespace StockTrader.Shared.Domain
{
    public abstract class UnaryValueObject<T> : ValueObject
    {
        public override string ToString() => GetValue()?.ToString() ?? "";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            var value = GetValue();
            if (value is not null) 
                yield return value;
        }

        protected abstract T GetValue();
    }
}