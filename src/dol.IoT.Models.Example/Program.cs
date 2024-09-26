using dol.IoT.Models.Example.Auth;
using dol.IoT.Models.Example.DeviceApi;
using dol.IoT.Models.Example.ManagementApi;
using dol.IoT.Models.Public.DeviceApi;

// Minimal example project for how to work with our HTTP API endpoints, which can be found here: https://dol-iot-api-qa.azurewebsites.net/swagger/index.html

// Email and Password for logging into our Integrator API, make sure to first register and test the login in Swagger in order to verify it works
using var client = new HttpClient(new LoginHandler
    {
        InnerHandler = new HttpClientHandler(),
        Email = "MyEmail@dol-sensors.com",
        Password = "SecretLongPassword123!"
    }
);

// Initialize DeviceApiClient, which contains selected usages of HTTP API endpoints in the "Device API" section of the Swagger page
var deviceApiClient = new DeviceApiClient { Client = client };

// Returns an instance of DeviceListResponse where allDeviceList.Devices is an array of DeviceInformationResponse containing information on all claimed devices
var allDeviceList = await deviceApiClient.AllDeviceList();

// The first three parameters, (mac, key, deviceType), needs to match the device you are trying to claim, and the values can be found on the physical device.
// The remaining two can be anything you want. ClaimDevice will throw an error if mac and key doesn't match or if the device has already been claimed
var claimDevice = await deviceApiClient.ClaimDevice("00mydeviceid", "MySecKey", DeviceType.IDOL65, "dol", "test device");

// Pass an array of claimed mac addresses and returns an array of DeviceOnlineWithIdResponse containing whether each device is online or offline
var deviceOnline = await deviceApiClient.DeviceOnline(["00mydeviceid"]);

// Returns an instance of DeviceResponse, which contains a lot of information about the device and its state
var deviceDetails = await deviceApiClient.DeviceDetails("00mydeviceid");

// Initialize DeviceApiClient, which contains selected usages of HTTP API endpoints in the "Device API" section of the Swagger page
var managementApiClient = new ManagementApiClient(client);

// PeekDataMessages returns List<DataMessage>, and PeekStatusMessages returns ServiceBusMessagesGrouped where each property is of the form List<T>
// where T are various message models that can be found in https://github.com/dol-sensors-A-S/dol.IoT.Models/tree/master/src/dol.IoT.Models/Messages
var dataResult = await managementApiClient.ReadDataMessages();
var statusResult = await managementApiClient.ReadStatusMessages();
