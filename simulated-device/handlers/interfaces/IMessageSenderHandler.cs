using Microsoft.Extensions.Logging;

namespace AzureIotSandbox.SimulatedDevice.Handlers
{
    public interface IMessageSenderHandler
    {
        Task Start();
    }
}