using System;

namespace StockTrader.Shared.Domain
{
    public abstract class Entity<TSelf, TId> : IEntity<TId>
        where TSelf : IEntity<TId>
        where TId : IIdentifier
    {
        protected Entity(TId id) =>
            Id = id ?? throw new ArgumentNullException(nameof(id));

        public TId Id { get; }

        IIdentifier IEntity.Id => Id;

        public static bool operator ==(Entity<TSelf, TId>? left, Entity<TSelf, TId>? right) =>
            Equals(left, right);

        public static bool operator !=(Entity<TSelf, TId>? left, Entity<TSelf, TId>? right) =>
            !(left == right);

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) ||
            (obj is not null && GetType() == obj.GetType() &&
             obj is IEntity other &&
             Id.Equals(other.Id));

        public override int GetHashCode() => unchecked((13 * GetType().GetHashCode()) ^ Id.GetHashCode());
    }
}
