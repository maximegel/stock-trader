using System;
using System.Runtime.Serialization;

namespace StockTrader.Shared.Domain
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
