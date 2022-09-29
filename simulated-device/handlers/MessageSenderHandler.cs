using System.Text;
using System.Text.Json;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

namespace AzureIotSandbox.SimulatedDevice.Handlers
{
    public class MessageSenderHandler : IMessageSenderHandler
    {
        private readonly DeviceClient _client;
        private readonly ILogger<IMessageSenderHandler> _logger;

        public MessageSenderHandler(DeviceClient client, ILogger<IMessageSenderHandler> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task Start()
        {
            Console.WriteLine("jioesfijop");
            _logger.LogInformation("Sendingfddsfsfd message");
            while(true)
            {
                string messageBody = JsonSerializer.Serialize(new{
                    temperature = 10.0,
                    id = 2
                });

                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
        
                _logger.LogInformation("Sending message");
                await _client.SendEventAsync(message);
                await Task.Delay(10000);
            }
        }
    }
}