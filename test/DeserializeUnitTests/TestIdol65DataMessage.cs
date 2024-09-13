using System.Text.Json;
using dol.IoT.Models.Public.Messages;

namespace Tests;

public class TestIdol65DataMessage
{
    public static IEnumerable<object[]> FullTestCases()
    {
        yield return
        [
            """
            {
                "id":"2c3f0d52-f532-456c-bac6-2230ca29dac6",
                "deviceId":"000ecd02c137",
                "sensorId":"000ecd02c137_Camera01_kg",
                "sensorName":"Camera01",
                "value":0.0,
                "type":"Weight",
                "unit":"kg",
                "timestamp":1724918880,
                "withinSpec":false,
                "count":0,
                "timespan":60,
                "countDelta":0
            }
            """,
            new DataMessage
            {
                Id = "2c3f0d52-f532-456c-bac6-2230ca29dac6",
                DeviceId = "000ecd02c137",
                SensorId = "000ecd02c137_Camera01_kg",
                SensorName = "Camera01",
                Value = 0.0m,
                Type = "Weight",
                Unit = "kg",
                Timestamp = 1724918880,
                WithinSpec = false,
                Count = 0,
                Timespan = 60,
                CountDelta = 0
            }
        ];
        yield return
        [
            """
            {
                "id":"2fbdaec1-2da4-4628-8a84-e31f6fb76319",
                "deviceId":"000ecd02c137",
                "sensorId":"000ecd02c137_Camera01_kg",
                "sensorName":"Camera01",
                "value":19.85,
                "type":"Weight",
                "unit":"kg",
                "timestamp":1724918340,
                "withinSpec":false,
                "count":212,
                "minWeight":5.31,
                "maxWeight":33.16,
                "timespan":60,
                "sd":12.95,
                "countDelta":30
            }
            """,
            new DataMessage
            {
                Id = "2fbdaec1-2da4-4628-8a84-e31f6fb76319",
                DeviceId = "000ecd02c137",
                SensorId = "000ecd02c137_Camera01_kg",
                SensorName = "Camera01",
                Value = 19.85m,
                Type = "Weight",
                Unit = "kg",
                Timestamp = 1724918340,
                WithinSpec = false,
                Count = 212,
                MinWeight = 5.31,
                MaxWeight = 33.16,
                Timespan = 60,
                Sd = 12.95,
                CountDelta = 30
            }
        ];
    }

    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    [Theory]
    [MemberData(nameof(FullTestCases))]
    public void DeserializeCalibrationTests(string message, DataMessage expected)
    {
        var messageDeserialized = JsonSerializer.Deserialize<DataMessage>(message, Options);
        Assert.Equal(JsonSerializer.Serialize(messageDeserialized), JsonSerializer.Serialize(expected));
    }
}