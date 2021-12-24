using Xunit;

namespace StockTrader.Portfolios.Api.IntegrationTests
{
    [CollectionDefinition("TestHost")]
    public class TestHostCollectionFixture : ICollectionFixture<TestHostFactory>
    {
    }
}
