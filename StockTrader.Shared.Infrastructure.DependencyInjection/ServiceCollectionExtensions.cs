using StockTrader.Shared.Application.Messaging;
using StockTrader.Shared.Infrastructure.Messaging.MediatR;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<ICommandBus, MediatorCommandBus>();
            services.AddScoped<IEventPublisher, MediatorEventPublisher>();
            return services;
        }
    }
}