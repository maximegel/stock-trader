using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Shared.Domain
{
    public abstract class ValueObject<TSelf>
        where TSelf : ValueObject<TSelf>
    {
        public static bool operator ==(ValueObject<TSelf>? left, ValueObject<TSelf>? right) => 
            Equals(left, right);

        public static bool operator !=(ValueObject<TSelf>? left, ValueObject<TSelf>? right) => 
            !(left == right);

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) ||
            obj is not null && GetType() == obj.GetType() && Equals(obj as ValueObject<TSelf>);

        public override int GetHashCode() =>
            GetEqualityComponents()
                .Select(obj => obj?.GetHashCode() ?? 0)
                .Aggregate(17, (hashCode, next) => unchecked((hashCode * 23) ^ next));

        protected abstract IEnumerable<object> GetEqualityComponents();

        private bool Equals(ValueObject<TSelf>? other) =>
            other != null &&
            GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }
}