namespace dol.IoT.Models.Public.Messages;

public class DataMessage
{
    public required string Id { get; set; }
    public required string DeviceId { get; set; }
    public required string SensorId { get; set; }
    public required string SensorName { get; set; }
    public decimal? Value { get; set; }
    public string? Data { get; set; }
    public required string Type { get; set; }
    public required string Unit { get; set; }
    public required long Timestamp { get; set; }

    public string? GatewayId { get; set; }
    public bool? WithinSpec { get; set; }
    public int? Count { get; set; }
    public double? MinWeight { get; set; }
    public double? MaxWeight { get; set; }
    public long? Timespan { get; set; }
    public double? Sd { get; set; }
    public int? CountDelta { get; set; }
    public double? Skewness { get; set; }
}