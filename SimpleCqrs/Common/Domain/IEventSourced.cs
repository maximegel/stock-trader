namespace SimpleCqrs.Common.Domain
{
    public interface IEventSourced<out TEvent>
        where TEvent : IDomainEvent
    {
        IEventSource<TEvent> UncommittedEvents { get; }
    }
}