using System;

namespace StockTrader.Common.Domain
{
    
    public sealed class Uuid : Identifier
    {
        private readonly Guid _value;
        
        internal Uuid(Guid value) =>
            _value = value;

        public static Uuid Default { get; } = new(Guid.Empty);

        public static Uuid Generate() =>
            new(Guid.NewGuid());
        
        public static Uuid Parse(string input) =>
            new(Guid.Parse(input));

        public static implicit operator Uuid(string value) =>
            Parse(value);

        protected override string GetValue() => 
            _value.ToString();
    }
    
    public abstract class Uuid<TSelf> : Identifier
        where TSelf : Uuid<TSelf>, new()
    {
        public static TSelf Default { get; } = new();
        
        private Uuid Value { get; init; } = Uuid.Default;
        
        public static TSelf Generate() => 
            new() {Value = Uuid.Generate()};

        public static TSelf Parse(string input) => 
            new() {Value = Uuid.Parse(input)};
        
        public static implicit operator Uuid<TSelf>(string value) =>
            Parse(value);

        protected override string GetValue() => 
            Value.ToString();
    }
}