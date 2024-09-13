namespace dol.IoT.Models.Public.Messages;

public class SensorBatteryUpdatesMessage
{
    public required string DeviceId { get; set; }
    public required long Timestamp { get; set; }
    public BatteryUpdate[]? BatteryUpdates { get; set; }
}

public class BatteryUpdate
{
    public required string DevEui { get; set; }
    public required int Code { get; set; }
    public required string BatteryStatus { get; set; }
}