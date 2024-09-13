using System.Text.Json.Serialization;

namespace dol.IoT.Models.Public.DeviceApi;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DeviceType
{
    Unknown = 0,
    IDOL63 = 1,
    IDOL64 = 2,
    IDOL65 = 3
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SensorType
{
    Unknown = 0,
    DOL16 = 1,
    DOL53 = 2,
    DOL90 = 3,
    IDOL139 = 4,
    DOL139 = 5,
    DOL114 = 6,
    DOL19 = 7,
    DOL104 = 8,
    Weight = 9,
    DOL51 = 10,
    Axetris_LGD_CompactA_CH4_Methane = 11,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WiredSensorType
{
    Unknown = 0,
    DOL16 = 1,
    DOL51 = 2,
    DOL53 = 3,
    DOL139 = 4,
    Axetris_LGD_CompactA_CH4_Methane = 5,
    SMC_Digital_Flow_Sensor_PF2M710 = 6
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CameraDirty
{
    Clean = 0,
    Dirty = 1,
    VeryDirty = 2,
}
