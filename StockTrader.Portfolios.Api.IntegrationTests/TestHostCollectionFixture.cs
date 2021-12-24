using Xunit;

namespace StockTrader.Portfolios.Api.IntegrationTests
{
    [CollectionDefinition("ApiTests")]
    public class TestHostCollectionFixture : ICollectionFixture<TestHostFactory>
    {
    }
}
