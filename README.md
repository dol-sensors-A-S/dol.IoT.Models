[![NuGet Version](https://img.shields.io/nuget/v/dol.IoT.Models?style=social&logo=nuget)](https://www.nuget.org/packages/dol.IoT.Models)

This repository contains an overview of all the various models when working with dol-sensors IoT integration endpoints, which can be found [here](https://dol-iot-api-qa.azurewebsites.net/swagger/index.html). You can find an introduction and a general overview of dol-sensors IoT integration [here](https://github.com/dol-sensors-A-S/dol.IoT.Integrator.Cli/blob/master/resources/Integrator-API-getting-started.md).

## `src/dol.IoT.Models`

This is where all the models are defined, and they all match the schemas that can be found in the Swagger page [here](https://dol-iot-api-qa.azurewebsites.net/swagger/index.html). The models are divided into 4 subdirectories, where the three subdirectories `Auth`, `DeviceApi`, `ManagementApi` match the three header in the Swagger page, and they contain all the models that are relevant for each of their corresponding endpoints. The subdirectory `Messages` contains all the models that are relevant for the HTTP API `/api/management/queue`.


## `src/dol.IoT.Models.Example`

This is a minimal example of how to work with the models in `src/dol.IoT.Models` in conjunction with our integration endpoints. It showcases how one can use the endpoints in the `Auth` section of our Swagger page to create `LoginHandler.cs`, found in the `Auth` subdirectory, which is used as a `HttpMessageHandler` in a  `HttpClient` to authenticate all HTTP API calls. `DeviceApiClient.cs` shows how to use the `HttpClient` to call some of the endpoints found in `Device API` section of our Swagger page. `ManagementApiClient.cs` shows how to use `/api/management/queue` GET request to retrieve status and data messages from all the claimed devices.