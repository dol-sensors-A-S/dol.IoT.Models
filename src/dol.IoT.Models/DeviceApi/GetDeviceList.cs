namespace dol.IoT.Models.Public.DeviceApi;

public record DeviceListResponse(DeviceInformationResponse[] Devices, int PageNumber, int PageSize, int DeviceTotal);
public record DeviceInformationResponse(string Mac, string DeviceName, DeviceType DeviceType, string CreatedAt);