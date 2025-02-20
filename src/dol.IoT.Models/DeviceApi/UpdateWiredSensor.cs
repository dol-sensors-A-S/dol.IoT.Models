namespace dol.IoT.Models.Public.DeviceApi;

public record UpdateWiredSensorsRequest(WiredSensorRequest[] Sensors);

public record WiredSensorRequest(string Name, int Port, WiredSensorType WiredSensorType, int SamplingRate, string LastSent);