namespace dol.IoT.Models.Public.DeviceApi;

public record DeviceResponse(
    string Mac,
    string Key,
    DeviceType DeviceType,
    string? Owner,
    string? DeviceName,
    string CreatedAt,
    string UpdatedAt,
    string ConnectionState,
    string FirmwareVersion,
    bool IsOnline,
    string? LastActivityUtc,
    int CloudToDeviceMessages,
    SensorResponse[]? Sensors,
    WiredSensorResponse[]? WiredSensors,
    CameraStatusResponse? CameraStatus,
    IDOL63ConfigResponse? Idol63Config);

public record CameraStatusResponse(
    CameraDirty? CameraDirty,
    bool? ManuallyCalibrated,
    string? CalibrationStatus,
    bool? DirtyDetectionEnabled,
    string? LastCalibrationTime,
    VisionMessageResponse[]? Messages);

public record VisionMessageResponse(int MessageId, string MessageText, string MessagePayload);

public record SensorResponse(
    string DevEui,
    string Name,
    string CreatedAt,
    string LatestDataSentAt,
    SensorType SensorType,
    int SampleRate,
    bool SampleRateInSync,
    BatteryStatus BatteryStatus);

public record BatteryStatus(int Code, string Value);

public record WiredSensorResponse(int Port, WiredSensorType WiredSensorType, int SamplingRate);

public record IDOL63ConfigResponse(IDOL63SensorConfigResponse[] Sensors, int SampleRate);

public record IDOL63SensorConfigResponse(string Name, int Port, string Type, string TypeName, string Unit);