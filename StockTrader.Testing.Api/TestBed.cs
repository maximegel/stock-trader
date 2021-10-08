namespace StockTrader.Testing.Api
{
    public class TestBed
    {
        private TestBed()
        {
        }

        public static TestBed Create { get; } = new();
    }
}
