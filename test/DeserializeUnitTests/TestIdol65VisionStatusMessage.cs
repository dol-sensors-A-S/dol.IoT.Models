using System.Text.Json;
using dol.IoT.Models.Public.Messages;

namespace Tests;

public class TestIdol65VisionStatusMessage
{
    private const string CalibrationStarted1 =
        """
        {
            "deviceId":"000ecd02c137",
            "visionStatus": {
                "calibrationLastUpdate":"2024-08-29T09:32:23Z",
                "calibrationUpdate":"Starting in-pen calibration"
            },
            "timestamp":1724923944
        }
        """;

    private const string CalibrationStarted2 =
        """
        {
            "deviceId":"000ecd02c137",
            "visionStatus": {
                "isDeviceManuallyCalibrated":"False",
                "calibration":"Started"
            },
            "timestamp":1724923944
        }
        """;

    private const string CalibrationOngoing =
        """
        {
            "deviceId":"000ecd02c137",
            "visionStatus": {
                "calibrationLastUpdate":"2024-08-29T09:33:10Z",
                "calibrationUpdate":"Image sampling done, starting image processing"
            },
            "timestamp":1724923991
        }
        """;

    private const string CalibrationSuccessful1 =
        """
        {
            "deviceId":"000ecd02c137",
            "visionStatus": {
                "calibrationLastUpdate":"2024-09-09T09:11:22Z",
                "messages": [
                    {
                        "messageId":0,
                        "messageText":"In-pen calibration successful ",
                        "messagePayload":""
                    }
                ]
            },
            "timestamp":1725873082
        }
        """;

    private const string CalibrationSuccessful2 =
        """
        {
            "deviceId":"000ecd02c137",
                "visionStatus": {
                    "calibration":"Done"
                },
            "timestamp":1724924048
        }
        """;

    private const string CalibrationFailed1 =
        """
        {
            "deviceId":"000ecd02c137",
            "visionStatus": {
                "calibrationLastUpdate":"2024-08-29T10:54:43Z",
                "messages": [
                    {
                        "messageId":3,
                        "messageText":"Installation height is below the specified minimum",
                        "messagePayload":"2199"
                    }
                ]
            },
            "timestamp":1724928883
        }
        """;

    private const string CalibrationFailed2 =
        """
        {
            "deviceId":"000ecd02c137",
            "visionStatus": {
                "calibrationLastUpdate":"2024-08-30T12:43:25Z",
                "messages": [
                    {
                        "messageId":4,
                        "messageText":"Installation height is above the specified maximum",
                        "messagePayload":"2500"
                    },
                    {
                        "messageId":5,
                        "messageText":"The camera is not parallel to the floor",
                        "messagePayload":"5"
                    },
                    {
                        "messageId":6,
                        "messageText":"The camera is not parallel to the floor",
                        "messagePayload":"5"
                    }
                ]
            },
            "timestamp":1725021805
        }
        """;

    private const string ReportClean =
        """
        {
            "deviceId":"000ecd02c137",
            "visionStatus": {
                "isDirty":"Clean"
            },
          "timestamp":1725024229
        }
        """;

    public static IEnumerable<object[]> FullTestCases()
    {
        yield return
        [
            CalibrationStarted1,
            new VisionStatusMessage
            {
                DeviceId = "000ecd02c137",
                VisionStatus = new VisionStatus
                    { CalibrationLastUpdate = "2024-08-29T09:32:23Z", CalibrationUpdate = "Starting in-pen calibration" },
                Timestamp = 1724923944
            }
        ];
        yield return
        [
            CalibrationStarted2,
            new VisionStatusMessage
            {
                DeviceId = "000ecd02c137",
                VisionStatus = new VisionStatus
                    { IsDeviceManuallyCalibrated = "False", Calibration = "Started" },
                Timestamp = 1724923944
            }
        ];
        yield return
        [
            CalibrationOngoing,
            new VisionStatusMessage
            {
                DeviceId = "000ecd02c137",
                VisionStatus = new VisionStatus
                {
                    CalibrationLastUpdate = "2024-08-29T09:33:10Z", CalibrationUpdate = "Image sampling done, starting image processing"
                },
                Timestamp = 1724923991
            }
        ];
        yield return
        [
            CalibrationSuccessful1,
            new VisionStatusMessage
            {
                DeviceId = "000ecd02c137",
                VisionStatus = new VisionStatus
                {
                    CalibrationLastUpdate = "2024-09-09T09:11:22Z",
                    Messages =
                    [
                        new VisionMessage
                            { MessageId = 0, MessageText = "In-pen calibration successful ", MessagePayload = "" }
                    ]
                },
                Timestamp = 1725873082
            }
        ];
        yield return
        [
            CalibrationSuccessful2,
            new VisionStatusMessage
            {
                DeviceId = "000ecd02c137",
                VisionStatus = new VisionStatus { Calibration = "Done" },
                Timestamp = 1724924048
            }
        ];
        yield return
        [
            CalibrationFailed1,
            new VisionStatusMessage
            {
                DeviceId = "000ecd02c137",
                VisionStatus = new VisionStatus
                {
                    CalibrationLastUpdate = "2024-08-29T10:54:43Z",
                    Messages =
                    [
                        new VisionMessage
                            { MessageId = 3, MessageText = "Installation height is below the specified minimum", MessagePayload = "2199" }
                    ]
                },
                Timestamp = 1724928883
            }
        ];
        yield return
        [
            CalibrationFailed2,
            new VisionStatusMessage
            {
                DeviceId = "000ecd02c137",
                VisionStatus = new VisionStatus
                {
                    CalibrationLastUpdate = "2024-08-30T12:43:25Z",
                    Messages =
                    [
                        new VisionMessage
                            { MessageId = 4, MessageText = "Installation height is above the specified maximum", MessagePayload = "2500" },
                        new VisionMessage
                            { MessageId = 5, MessageText = "The camera is not parallel to the floor", MessagePayload = "5" },
                        new VisionMessage
                            { MessageId = 6, MessageText = "The camera is not parallel to the floor", MessagePayload = "5" }
                    ]
                },
                Timestamp = 1725021805
            }
        ];
        yield return
        [
            ReportClean,
            new VisionStatusMessage
            {
                DeviceId = "000ecd02c137",
                VisionStatus = new VisionStatus
                {
                    IsDirty = "Clean"
                },
                Timestamp = 1725024229
            }
        ];
    }

    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    [Theory]
    [MemberData(nameof(FullTestCases))]
    public void DeserializeCalibrationTests(string message, VisionStatusMessage expected)
    {
        var messageDeserialized = JsonSerializer.Deserialize<VisionStatusMessage>(message, Options);
        Assert.Equal(JsonSerializer.Serialize(messageDeserialized), JsonSerializer.Serialize(expected));
    }
}