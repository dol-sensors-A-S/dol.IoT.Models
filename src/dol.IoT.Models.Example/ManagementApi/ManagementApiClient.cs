using System.Net.Http.Json;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using dol.IoT.Models.Public.ManagementApi;
using dol.IoT.Models.Public.Messages;

namespace dol.IoT.Models.Example.ManagementApi;

public class ManagementApiClient
{
    private QueueConnectionInfoResponse? QueueResponse { get; set; }

    private ServiceBusClient DataServiceBusClient { get; set; }
    private ServiceBusClient StatusServiceBusClient { get; set; }
    private readonly ServiceBusReceiverOptions _serviceBusReceiverOptions = new() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete };

    private const string ManagementQueueUri = "https://dol-iot-api-qa.azurewebsites.net/api/management/queue";

    public ManagementApiClient(HttpClient client)
    {
        QueueResponse = client.GetFromJsonAsync<QueueConnectionInfoResponse>(ManagementQueueUri).GetAwaiter().GetResult();
        DataServiceBusClient = new ServiceBusClient(QueueResponse!.DataQueueConnection);
        StatusServiceBusClient = new ServiceBusClient(QueueResponse.StatusQueueConnection);
    }

    public async Task<List<DataMessage>> ReadDataMessages(int messagesToPeek = int.MaxValue, bool consumeMessages = false)
    {
        var dataReceiver = DataServiceBusClient.CreateReceiver(QueueResponse!.DataQueueName, _serviceBusReceiverOptions);
        IReadOnlyList<ServiceBusReceivedMessage>? serviceBusMessages;
        List<DataMessage> dataMessages = [];

        do
        {
            serviceBusMessages = consumeMessages
                ? await dataReceiver.ReceiveMessagesAsync(messagesToPeek, TimeSpan.FromSeconds(5))
                : await dataReceiver.PeekMessagesAsync(messagesToPeek);

            foreach (var message in serviceBusMessages)
            {
                ServiceBusMessagesGrouped.DeserializeAndAdd(dataMessages, message.Body);
            }

            messagesToPeek -= serviceBusMessages.Count;
        } while (serviceBusMessages.Count > 0 && messagesToPeek > 0);

        return dataMessages;
    }

    public async Task<ServiceBusMessagesGrouped> ReadStatusMessages(int messagesToPeek = int.MaxValue, bool consumeMessages = false)
    {
        var statusReceiver = StatusServiceBusClient.CreateReceiver(QueueResponse!.StatusQueueName, _serviceBusReceiverOptions);
        IReadOnlyList<ServiceBusReceivedMessage>? serviceBusMessages;
        var statusMessagesGrouped = new ServiceBusMessagesGrouped();

        do
        {
            serviceBusMessages = consumeMessages
                ? await statusReceiver.ReceiveMessagesAsync(messagesToPeek, TimeSpan.FromSeconds(5))
                : await statusReceiver.PeekMessagesAsync(messagesToPeek);

            foreach (var message in serviceBusMessages)
            {
                statusMessagesGrouped.AddBinaryDataToCorrespondingList(message.Subject, message.Body);
            }

            messagesToPeek -= serviceBusMessages.Count;
        } while (serviceBusMessages.Count > 0 && messagesToPeek > 0);

        return statusMessagesGrouped;
    }
}

public class ServiceBusMessagesGrouped
{
    public List<DeviceConnectionMessage> DeviceConnectionMessages { get; set; } = [];
    public List<VisionStatusMessage> VisionStatusMessage { get; set; } = [];
    public List<SensorInactiveMessage> SensorInactiveMessage { get; set; } = [];
    public List<SensorBatteryUpdatesMessage> SensorBatteryUpdatesMessage { get; set; } = [];

    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    public void AddBinaryDataToCorrespondingList(string subject, BinaryData data)
    {
        switch (subject)
        {
            case "DeviceConnectionChanged":
                DeserializeAndAdd(DeviceConnectionMessages, data);
                break;
            case "VisionStatusMessage":
                DeserializeAndAdd(VisionStatusMessage, data);
                break;
            case "SensorInactive":
                DeserializeAndAdd(SensorInactiveMessage, data);
                break;
            case "SensorBatteryUpdates":
                DeserializeAndAdd(SensorBatteryUpdatesMessage, data);
                break;
        }
    }

    public static void DeserializeAndAdd<T>(List<T> modelList, BinaryData data)
    {
        modelList.Add(JsonSerializer.Deserialize<T>(data, Options)!);
    }
}