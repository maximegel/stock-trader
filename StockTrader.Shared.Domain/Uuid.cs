using System;

namespace StockTrader.Shared.Domain
{
    public abstract class Uuid<TSelf> : Identifier<TSelf, Guid>
        where TSelf : Uuid<TSelf>, new()
    {
        protected Uuid() : 
            base(Guid.Empty) { }
        
        protected Uuid(Guid value) : 
            base(value) { }
        
        public static TSelf Default { get; } = new();
        
        public static TSelf Generate() => 
            new() {Value = Guid.NewGuid()};

        public static TSelf Parse(string input) => 
            new() {Value = Guid.Parse(input)};
        
        public static implicit operator Uuid<TSelf>(string value) =>
            Parse(value);
    }
}