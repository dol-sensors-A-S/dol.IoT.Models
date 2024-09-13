namespace dol.IoT.Models.Public.DeviceApi;

public record ClaimDeviceRequest(
    string MacAddress,
    string Key,
    DeviceType DeviceType,
    string? Owner,
    string? DeviceName);

public record ClaimDeviceResponse(
    string MacAddress,
    string Integrator);