using System.Linq;

namespace SimpleCqrs.Common.Domain
{
    public abstract class Identifier : ValueObject
    {
        public static implicit operator object[](Identifier self) =>
            self.GetEqualityComponents().ToArray();
    }
}