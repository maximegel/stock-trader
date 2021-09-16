using System;

namespace StockTrader.Shared.Domain
{
    public abstract class Entity<TId> : Entity, IEntity<TId>
        where TId : Identifier
    {
        protected Entity(TId id) =>
            Id = id ?? throw new ArgumentNullException(nameof(id));
        
        public TId Id { get; }
        
        protected override Identifier GetId() => Id;
    }
    
    public abstract class Entity : IEntity, IEquatable<IEntity>
    {
        Identifier IEntity.GetId() => GetId();

        public bool Equals(IEntity? other) =>
            GetId().Equals(other?.GetId());

        public static bool operator ==(Entity? left, Entity? right) => Equals(left, right);

        public static bool operator !=(Entity? left, Entity? right) => !(left == right);

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) ||
            obj is not null && GetType() == obj.GetType() && Equals(obj as IEntity);

        public override int GetHashCode() => unchecked((13 * GetType().GetHashCode()) ^ GetId().GetHashCode());

        protected abstract Identifier GetId();
    }
}