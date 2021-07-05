using System;

namespace SimpleCqrs.Common.Domain
{
    public abstract class Entity<TId> : IEntity
    {
        protected Entity(TId id) => Id = id ?? throw new ArgumentNullException(nameof(id));

        public TId Id { get; protected init; }

        string IEntity.Id() => Id.ToString();

        public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);

        public static bool operator !=(Entity<TId> left, Entity<TId> right) => !(left == right);

        public override bool Equals(object obj) =>
            ReferenceEquals(this, obj) ||
            obj is not null && GetType() == obj.GetType() && Equals(obj as Entity<TId>);

        public override int GetHashCode() => unchecked((13 * GetType().GetHashCode()) ^ (Id?.GetHashCode() ?? 0));
    }
}