using System;

namespace SimpleCqrs.Common.Domain
{
    public abstract class Entity<TId> : IEntity
        where TId : Identifier
    {
        protected Entity(TId id) => Id = id ?? throw new ArgumentNullException(nameof(id));

        public TId Id { get; }

        Identifier IEntity.Id => Id;

        public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);

        public static bool operator !=(Entity<TId> left, Entity<TId> right) => !(left == right);

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) ||
            obj is not null && GetType() == obj.GetType() && Id.Equals((obj as IEntity)?.Id);

        public override int GetHashCode() => unchecked((13 * GetType().GetHashCode()) ^ Id.GetHashCode());
    }
}