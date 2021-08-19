using System.Collections.Generic;
using System.Linq;

namespace SimpleCqrs.Common.Domain
{
    public abstract class ValueObject
    {
        public static bool operator ==(ValueObject left, ValueObject right) => Equals(left, right);

        public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) ||
            obj is not null && GetType() == obj.GetType() && Equals(obj as ValueObject);

        public override int GetHashCode()
        {
            return unchecked(GetEqualityComponents()
                .Aggregate(17, (current, obj) => (current * 23) ^ (obj?.GetHashCode() ?? 0)));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}