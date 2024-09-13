namespace dol.IoT.Models.Public.DeviceApi;

public record UpdateWiredSensorsRequest(WiredSensorRequest[] Sensors);
public record WiredSensorRequest(int Port, WiredSensorType WiredSensorType, int SamplingRate);