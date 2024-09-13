namespace dol.IoT.Models.Public.Messages;

public class SensorInactiveMessage
{
    public required string DeviceId { get; set; }
    public required InactiveSensor[] InactiveSensors { get; set; }
    public required long Timestamp { get; set; }
}

public class InactiveSensor
{
    public required string Name { get; set; }
    public required string DevEui { get; set; }
    public required string LastSeenAt { get; set; }
}