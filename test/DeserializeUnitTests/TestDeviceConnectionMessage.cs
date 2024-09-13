using System.Text.Json;
using dol.IoT.Models.Public.Messages;

namespace Tests;

public class TestDeviceConnectionMessage
{
    private const string ConnectionChangesMessage =
        """
        {
            "deviceId":"000ecd02c137",
            "state":"deviceConnected",
            "timestamp":1725403912
        }
        """;

    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    [Fact]
    public void TestDeserialization()
    {
        var deviceConnectionChanged = JsonSerializer.Deserialize<DeviceConnectionMessage>(ConnectionChangesMessage, Options);
        Assert.Equal("000ecd02c137", deviceConnectionChanged!.DeviceId);
        Assert.Equal("deviceConnected", deviceConnectionChanged.State);
        Assert.Equal(1725403912, deviceConnectionChanged.Timestamp);
    }

}