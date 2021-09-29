using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StockTrader.Api;

namespace StockTrader.Portfolios.Api.IntegrationTests
{
    public class PortfoliosApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder
                .UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    PortfoliosDataInitializer.Initialize(scopedServices);
                });
        }
    }
}