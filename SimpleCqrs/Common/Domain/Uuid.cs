using System;
using System.Collections.Generic;

namespace SimpleCqrs.Common.Domain
{
    public abstract class Uuid<TSelf> : Identifier
        where TSelf : Uuid<TSelf>, new()
    {
        private Guid Id { get; init; }

        public static TSelf Generate() => new() {Id = Guid.NewGuid()};

        public static TSelf Parse(string input) => new() {Id = Guid.Parse(input)};

        public override string ToString() => Id.ToString();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}