using System.Text;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;

namespace AzureIotSandbox.SimulatedDevice.Handlers
{
    public class C2dMessageReceiverHandler : IC2dMessageReceiverHandler
    {
        private readonly DeviceClient _client;
        private readonly ILogger<IC2dMessageReceiverHandler> _logger;

        public C2dMessageReceiverHandler(DeviceClient client, ILogger<IC2dMessageReceiverHandler> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task Setup()
        {
            _logger.LogInformation("Setting up C2D handler");
            await _client.SetReceiveMessageHandlerAsync(OnC2dMessageReceived, _client);
        }

        private async Task OnC2dMessageReceived(Message message, object _)
        {
            _logger.LogInformation($"{DateTime.Now} > Received message with id: {message.MessageId}");
            PrintMessage(message);

            await _client.CompleteAsync(message);
        }

        private void PrintMessage(Message receivedMessage)
        {
            string messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
            var formattedMessage = new StringBuilder($"Received message: [{messageData}]\n");

            // User set application properties can be retrieved from the Message.Properties dictionary.
            foreach (KeyValuePair<string, string> prop in receivedMessage.Properties)
            {
                formattedMessage.AppendLine($"\tProperty: key={prop.Key}, value={prop.Value}");
            }
            // System properties can be accessed using their respective accessors, e.g. DeliveryCount.
            formattedMessage.AppendLine($"\tDelivery count: {receivedMessage.DeliveryCount}");

            _logger.LogInformation($"{DateTime.Now}> {formattedMessage}");
        }

    }
}
