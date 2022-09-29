using System.Text;
using System.Text.Json;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzureIotSandbox.SimulatedDevice.Handlers
{
    public class MessageSenderHandler : BackgroundService
    {
        private readonly DeviceClient _client;
        private readonly ILogger<MessageSenderHandler> _logger;

        public MessageSenderHandler(DeviceClient client, ILogger<MessageSenderHandler> logger)
        {
            _client = client;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
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