using System;

namespace StockTrader.Shared.Domain
{
    public abstract class Entity<TId> : IEntity<TId>
        where TId : IIdentifier
    {
        protected Entity(TId id) =>
            Id = id ?? throw new ArgumentNullException(nameof(id));

        public TId Id { get; }

        IIdentifier IEntity.Id => Id;

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) ||
            (obj is not null && GetType() == obj.GetType() &&
             obj is IEntity other &&
             Equals(other));

        public override int GetHashCode() => unchecked((13 * GetType().GetHashCode()) ^ Id.GetHashCode());
    }
}
