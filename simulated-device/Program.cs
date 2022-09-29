using System;
using System.Text;
using System.Text.Json;
using AzureIotSandbox.SimulatedDevice.Handlers;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzureIotSandbox.SimulatedDevice
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            await Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddLogging(l => l.AddConsole())
                        .AddSingleton<DeviceClient>(c => DeviceClient.CreateFromConnectionString(""))
                        .AddHostedService<MessageSenderHandler>()
                        .AddHostedService<C2dMessageReceiverHandler>();
                })
                .Build()
                .RunAsync();
        }
    }
}