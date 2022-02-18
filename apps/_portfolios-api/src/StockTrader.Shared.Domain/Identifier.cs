namespace StockTrader.Shared.Domain
{
    public abstract class Identifier<TSelf, TValue> : UnaryValueObject<TSelf, TValue>,
        IIdentifier
        where TSelf : Identifier<TSelf, TValue>
    {
        protected Identifier(TValue value)
            : base(value)
        {
        }

        public static implicit operator string(Identifier<TSelf, TValue> self) =>
            self.ToString();

        public static explicit operator TValue(Identifier<TSelf, TValue> self) =>
            self.Value;
    }
}
