using System;
using System.Text;
using System.Text.Json;
using AzureIotSandbox.SimulatedDevice.Handlers;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureIotSandbox.SimulatedDevice
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
                .AddLogging(l => l.AddConsole())
                .AddSingleton<DeviceClient>(c => DeviceClient.CreateFromConnectionString("f"))
                .AddSingleton<IMessageSenderHandler, MessageSenderHandler>()
                .AddSingleton<IC2dMessageReceiverHandler, C2dMessageReceiverHandler>()
                .BuildServiceProvider();


            //await serviceProvider.GetService<IMessageSenderHandler>().Start();
            await serviceProvider.GetService<IC2dMessageReceiverHandler>().Setup();

            while(true){}

           //var device = new RunDevice();

           //await device.Run();
        }
    }
}