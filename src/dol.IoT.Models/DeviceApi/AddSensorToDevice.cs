namespace dol.IoT.Models.Public.DeviceApi;

public record AddSensorToDeviceRequest(
    string DevEui,
    string Name,
    SensorType Type,
    int SampleRate = 600,
    SensorDetailsRequest? SensorDetailsRequest = null);

public record SensorDetailsRequest(
    string? ProductName,
    string? ProductionVersion,
    string? SerialNumber);