using System;

namespace dol.IoT.Models.Public.Messages;

public class VisionStatusMessage
{
    public required string DeviceId { get; set; }
    public required VisionStatus VisionStatus { get; set; }
    public long Timestamp { get; set; }
}

public class VisionStatus
{
    public string? IsDirty { get; set; }
    public string? IsDeviceManuallyCalibrated { get; set; }
    public string? Calibration { get; set; }
    public string? IsDetectingDirty { get; set; }
    public string? CalibrationLastUpdate { get; set; }
    public string? CalibrationUpdate { get; set; }
    public VisionMessage[] Messages { get; set; } = Array.Empty<VisionMessage>();
    public string? LastWeightCacheClearCount { get; set; }
    public string? LastWeightCacheClearedUpdate { get; set; }
}

public class VisionMessage
{
    public int MessageId { get; set; }
    public string? MessageText { get; set; }
    public string? MessagePayload { get; set; }
}