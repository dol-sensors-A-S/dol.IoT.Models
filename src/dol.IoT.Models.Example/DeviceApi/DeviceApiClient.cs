using System.Net.Http.Json;
using dol.IoT.Models.Public.DeviceApi;

namespace dol.IoT.Models.Example.DeviceApi;

public class DeviceApiClient
{
    public HttpClient Client { get; init; } = null!;

    private async Task<T?> GetAsync<T>(string uri)
    {
        var httpResponseMessage = await Client.GetAsync(uri);
        httpResponseMessage.EnsureSuccessStatusCode();

        return await httpResponseMessage.Content.ReadFromJsonAsync<T>();
    }

    public async Task<DeviceListResponse?> AllDeviceList(int page = 1, int pageSize = 100)
    {
        var allListUri = $"https://dol-iot-api-qa.azurewebsites.net/api/devices?page={page}&pageSize={pageSize}";

        return await GetAsync<DeviceListResponse>(allListUri);
    }

    public async Task<DeviceResponse?> DeviceDetails(string mac)
    {
        var deviceDetailsUri = $"https://dol-iot-api-qa.azurewebsites.net/api/devices/{mac}";

        return await GetAsync<DeviceResponse>(deviceDetailsUri);
    }

    public async Task<ClaimDeviceResponse?> ClaimDevice(string mac, string key, DeviceType deviceType, string? owner, string? deviceName)
    {
        const string claimDeviceUri = "https://dol-iot-api-qa.azurewebsites.net/api/devices/claim";
        ClaimDeviceRequest request = new(MacAddress: mac, Key: key, DeviceType: deviceType, Owner: owner, DeviceName: deviceName);

        var httpResponseMessage = await Client.PostAsJsonAsync(claimDeviceUri, request);
        httpResponseMessage.EnsureSuccessStatusCode();

        return await httpResponseMessage.Content.ReadFromJsonAsync<ClaimDeviceResponse>();
    }

    public async Task<DeviceOnlineWithIdResponse[]?> DeviceOnline(string[] macAddresses)
    {
        var macAddressesQuery = string.Join("&mac=", macAddresses);
        var deviceOnlineUri = $"https://dol-iot-api-qa.azurewebsites.net/api/devices/online?mac={macAddressesQuery}";

        return await GetAsync<DeviceOnlineWithIdResponse[]>(deviceOnlineUri);
    }
}