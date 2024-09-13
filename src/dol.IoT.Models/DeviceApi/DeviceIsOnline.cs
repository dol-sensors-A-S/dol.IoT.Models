namespace dol.IoT.Models.Public.DeviceApi;

public record DeviceOnlineResponse(
    string Mac,
    bool IsOnline);