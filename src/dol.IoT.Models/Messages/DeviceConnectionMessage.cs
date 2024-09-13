namespace dol.IoT.Models.Public.Messages;

public class DeviceConnectionMessage
{
    public required string DeviceId { get; set; }
    public required string State { get; set; }
    public required long Timestamp { get; set; }
}