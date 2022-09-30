using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzureIotSandbox.SimulatedDevice
{
    public class DeviceTwinHandler : BackgroundService
    {
        private DeviceClient _deviceClient;
        private ILogger<DeviceTwinHandler> _logger;
    

        public DeviceTwinHandler(DeviceClient deviceClient, ILogger<DeviceTwinHandler> logger)
        {
            _deviceClient = deviceClient;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _deviceClient.SetDesiredPropertyUpdateCallbackAsync(OnDesiredPropertyChangedAsync, null);
        }

        private async Task OnDesiredPropertyChangedAsync(TwinCollection desiredProperties, object userContext)
        {
           var reportedProperties = new TwinCollection();

            _logger.LogInformation("\tDesired properties requested:");
            _logger.LogInformation($"\t{desiredProperties.ToJson()}");

            // For the purpose of this sample, we'll blindly accept all twin property write requests.
            foreach (KeyValuePair<string, object> desiredProperty in desiredProperties)
            {
                _logger.LogInformation($"Setting {desiredProperty.Key} to {desiredProperty.Value}.");
                reportedProperties[desiredProperty.Key] = desiredProperty.Value;
            }

            _logger.LogInformation("\tAlso setting current time as reported property");
            reportedProperties["DateTimeLastDesiredPropertyChangeReceived"] = DateTime.UtcNow;

            await _deviceClient.UpdateReportedPropertiesAsync(reportedProperties);
        }
    }
}