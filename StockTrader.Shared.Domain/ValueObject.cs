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

        public override int GetHashCode()
        {
            return unchecked(GetEqualityComponents()
                .Aggregate(17, (current, obj) => (current * 23) ^ (obj?.GetHashCode() ?? 0)));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}