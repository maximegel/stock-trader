using System;
using System.Collections.Generic;
using SimpleCqrs.Common.Domain;

namespace SimpleCqrs.Domain
{
    public class MoneyAmount : ValueObject, IComparable<MoneyAmount>, IComparable
    {
        private readonly decimal _value;

        public MoneyAmount(decimal value) => _value = value;

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is MoneyAmount other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(MoneyAmount)}");
        }

        public int CompareTo(MoneyAmount other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }

        public static bool operator <(MoneyAmount left, MoneyAmount right) =>
            Comparer<MoneyAmount>.Default.Compare(left, right) < 0;

        public static bool operator >(MoneyAmount left, MoneyAmount right) =>
            Comparer<MoneyAmount>.Default.Compare(left, right) > 0;

        public static bool operator <=(MoneyAmount left, MoneyAmount right) =>
            Comparer<MoneyAmount>.Default.Compare(left, right) <= 0;

        public static bool operator >=(MoneyAmount left, MoneyAmount right) =>
            Comparer<MoneyAmount>.Default.Compare(left, right) >= 0;

        public static MoneyAmount operator +(MoneyAmount left) => left;

        public static MoneyAmount operator -(MoneyAmount left) => new(-left._value);

        public static MoneyAmount operator +(MoneyAmount left, MoneyAmount right) => new(left._value + right._value);

        public static MoneyAmount operator -(MoneyAmount left, MoneyAmount right) => left + -right;

        public override string ToString() => _value.ToString("C");

        protected override IEnumerable<object> GetEqualityValues()
        {
            yield return _value;
        }
    }
}