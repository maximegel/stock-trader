using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockTrader.Api;
using Xunit.Abstractions;

namespace StockTrader.Portfolios.Api.IntegrationTests
{
    public abstract class EndpointTests : IDisposable
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly Lazy<IServiceScope> _scope;
        private readonly HttpClient _client;

        protected EndpointTests(
            WebApplicationFactory<Startup> factory,
            ITestOutputHelper output)
        {
            _factory = factory.WithWebHostBuilder(host =>
                host.ConfigureTestServices(services => { LoggingServiceCollectionExtensions.AddLogging(services, logging => XUnitLoggerExtensions.AddXUnit((ILoggingBuilder)logging, output)); }));
            _scope = new Lazy<IServiceScope>(() => _factory.Services.CreateScope());
            _client = _factory.CreateClient();
        }

        protected IServiceProvider Services =>
            _scope.Value.ServiceProvider;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected async Task<HttpResponseMessage> When(
            Func<HttpClient, Task<HttpResponseMessage>> func)
        {
            return await func(_client);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_scope.IsValueCreated)
            {
                _scope.Value.Dispose();
            }

            _factory.Dispose();
        }
    }
}
