using System;

namespace dol.IoT.Models.Public.ManagementApi;

public record RequeueDeviceDataRequest(string Mac, DateTime FromUtcTime, DateTime? ToUtcTime);