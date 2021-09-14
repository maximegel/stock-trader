using System.Collections.Generic;

namespace SimpleCqrs.Common.Domain
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