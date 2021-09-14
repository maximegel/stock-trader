using System;

namespace SimpleCqrs.Common.Domain
{
    public interface IEntity
    {
        Identifier GetId();
    }

    public interface IEntity<out TId> : IEntity
        where TId : Identifier
    {
        TId Id { get; }
    }
}