namespace dol.IoT.Models.Public.DeviceApi;

public record UpdateSensorToDeviceRequest(string DevEui, string? Name, int? SampleRate);