using System;

namespace SimpleCqrs.Common.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}