using System.Text.Json.Serialization;

namespace dol.IoT.Models.Public.DeviceApi;

public record UpdateIdol63Request(Idol63Sensor[] Sensors, int SampleRate);

public record Idol63Sensor(string Name, int Port, Idol63SensorType Type);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Idol63SensorType
{
    Unknown = 0,
    DOL114_Temperature = 1,
    DOL114_Humidity = 2,
    DOL19_CO2 = 3,
    DOL53_NH3 = 4,
    DOL16_LUX_100 = 5,
    DOL114_LUX_1000 = 6,
    DOL104_Humidity = 7,
    DOL90_Water = 8,
    DOL139_Temperature = 10,
    DOL139_Humidity = 11,
    DOL139_CO2 = 12,
}