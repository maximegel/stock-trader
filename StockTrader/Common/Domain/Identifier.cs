using System;

namespace StockTrader.Common.Domain
{
    public abstract class Identifier : UnaryValueObject<string>
    {
        public static implicit operator Identifier(Guid value) =>
            new Uuid(value);
        
        public static implicit operator Identifier(int value) =>
            new Uid(value);
        
        public static implicit operator Identifier(long value) =>
            new Uid(value);
        
        public static implicit operator string(Identifier self) =>
            self.GetValue();

        public static explicit operator Guid(Identifier self) =>
            Guid.Parse(self.GetValue());

        public static explicit operator long(Identifier self) =>
            long.Parse(self.GetValue());
    }
}