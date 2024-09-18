namespace dol.IoT.Models.Public.DeviceApi;

public record EditClaimDeviceRequest(
    string MacAddress,
    string? Owner,
    string? DeviceName);