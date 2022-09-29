using System.Text;
using System.Text.Json;
using Microsoft.Azure.Devices.Client;

namespace AzureIotSandbox.SimulatedDevice
{
    public class RunDevice
    {
        private DeviceClient _client;
        public RunDevice()
        {
            _client = DeviceClient.CreateFromConnectionString("HostName=iothubrds.azure-devices.net;DeviceId=sandbox;SharedAccessKey=WcRY54R2RD7lefmLd6ZIBB2IgMh9v0d2zR4noNj365g=");
        }

        public async Task Run()
        {
             string messageBody = JsonSerializer.Serialize(new{
                temperature = 10.0,
                id = 2
            });

            await _client.SetReceiveMessageHandlerAsync(OnC2dMessageReceived, _client);

            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            while (true)
            {
                Console.WriteLine("Sending Message");
                await _client.SendEventAsync(message);
                await Task.Delay(1000);
            }
        }

        private async Task OnC2dMessageReceived(Message message, object _)
        {
            Console.WriteLine($"{DateTime.Now} > Received message with id: {message.MessageId}");
            PrintMessage(message);

            await _client.CompleteAsync(message);


        }

        private static void PrintMessage(Message receivedMessage)
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

            Console.WriteLine($"{DateTime.Now}> {formattedMessage}");
        }
    }
}