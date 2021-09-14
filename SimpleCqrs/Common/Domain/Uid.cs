using System;

namespace SimpleCqrs.Common.Domain
{
    public sealed class Uid : Identifier
    {
        private readonly long _value;

        internal Uid(int value) :
            this((long) value) { }

        internal Uid(long value)
        {
            if (value < 0)
                throw new ArgumentException("Uid must be a positive number.", nameof(value));
            _value = value;
        }
        
        public static Uid Default { get; } = new(0);
        
        public static Uid Parse(string input) =>
            new(long.Parse(input));
        
        public static implicit operator Uid(string value) =>
            Parse(value);

        protected override string GetValue() =>
            _value.ToString();
    }
    
    public abstract class Uid<TSelf> : Identifier
        where TSelf : Uid<TSelf>, new()
    {
        public static TSelf Default { get; } = new();

        private Uid Value { get; init; } = Uid.Default;

        public static TSelf Parse(string input) => 
            new() {Value = Uid.Parse(input)};
        
        public static implicit operator Uid<TSelf>(string value) =>
            Parse(value);

        protected override string GetValue() =>
            Value.ToString();
    }
}