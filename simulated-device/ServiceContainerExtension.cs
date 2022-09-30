using AzureIotSandbox.SimulatedDevice.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace AzureIotSandbox.SimulatedDevice
{
    public static class ServiceContainerExtension
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddHostedService<MessageSenderHandler>();
            services.AddHostedService<C2dMessageReceiverHandler>();
            services.AddHostedService<DeviceTwinHandler>();

            return services;
        }
    }
}