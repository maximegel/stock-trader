using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace StockTrader.Shared.Api.Testing
{
    [Collection("ApiTests")]
    public abstract class EndpointTests<TEntryPoint> : IDisposable
        where TEntryPoint : class
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<TEntryPoint> _factory;
        private readonly Lazy<IServiceScope> _scope;

        protected EndpointTests(
            WebApplicationFactory<TEntryPoint> factory,
            ITestOutputHelper output)
        {
            _factory = factory.WithWebHostBuilder(host =>
                host.ConfigureTestServices(services =>
                {
                    //services.AddLogging(logging => logging.AddXUnit(output));
                    ConfigureTestServices(services);
                }));
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
            Func<HttpClient, Task<HttpResponseMessage>> func) =>
            await func(_client);

        protected virtual void ConfigureTestServices(IServiceCollection services)
        {
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
