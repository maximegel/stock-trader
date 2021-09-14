namespace SimpleCqrs.Common.Domain
{
    public interface ICommand
    {
        string AggregateId { get; }
    }
}