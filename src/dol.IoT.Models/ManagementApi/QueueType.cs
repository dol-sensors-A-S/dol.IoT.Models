using System.Text.Json.Serialization;

namespace dol.IoT.Models.Public.ManagementApi;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QueueType
{
    Data = 0,
    Status = 1,
}