# Status messages
## Connection 
Messages related to device's connection status will have message property subject `DeviceConnectionChanged`.
The device will continuously monitor its connection status, and whenever the connection status has changed, the following message will be sent.

```json
{
  "deviceId":"000ecd02c137",
  "state":"deviceDisconnected",
  "timestamp":1725263509
}
```

`"state"` has two values: `"deviceDisconnected"` and `"deviceConnected"`.

## Calibration

Messages related to calibration will have message property subject `VisionStatusMessage`.
Calibration is the process in which the camera samples through 450 images in order to compute a plane equation of the floor.
When triggering calibration, and the device has received this calibration trigger, the first two messages that will be sent by the device will be the following json objects.

```json
{
  "deviceId":"000ecd02c137",
  "visionStatus": {
    "calibrationLastUpdate":"2024-08-29T09:32:23Z",
    "calibrationUpdate":"Starting in-pen calibration"
  },
  "timestamp":1724923944
}
```

```json
{
  "deviceId":"000ecd02c137",
  "visionStatus": {
    "isDeviceManuallyCalibrated":"False",
    "calibration":"Started"
  },
  "timestamp":1724923944
}
```

After the calibration has been started, there will be intermediate messages sent from the device which gives and update of how far it is during the calibration process. The intermediate messages will all have the following format.

```json
{
  "deviceId":"000ecd02c137",
  "visionStatus": {
    "calibrationLastUpdate":"2024-08-29T09:33:10Z",
    "calibrationUpdate":"Image sampling done, starting image processing"
  },
  "timestamp":1724923991
}
```

The final two messages that will be published when calibration is done will look like this:

```json
{
  "deviceId":"000ecd02c137",
  "visionStatus": {
    "calibrationLastUpdate":"2024-09-09T09:11:22Z",
    "messages": [
      {
        "messageId":0,
        "messageText":"In-pen calibration successful ",
        "messagePayload":""
      }
    ]
  },
  "timestamp":1725873082
}
```

```json
{
  "deviceId":"000ecd02c137",
  "visionStatus": {
    "calibration":"Done"
  },
  "timestamp":1724924048
}
```

If calibration failed the final two messages will instead look like this:

```json
{
  "deviceId":"000ecd02c137",
  "visionStatus": {
    "calibrationLastUpdate":"2024-08-29T10:54:43Z",
    "messages": [
      {
        "messageId":3,
        "messageText":"Installation height is below the specified minimum",
        "messagePayload":"2199"
      }
    ]
  },
  "timestamp":1724928883
}
```

```json
{
  "deviceId":"000ecd02c137",
  "visionStatus": {
    "calibration":"Required"
  },
  "timestamp":1724928883
}
```

`visionStatus.calibration` will only be reported as `"Done"` if calibration was successful, otherwise it will always be reported as `"Required"`.

The following list contains all the different `visionStatus.messages` that can be sent from the device.

- `"messages":[{"messageId":0,"messageText":"In-pen calibration successful ","messagePayload":""}]`
- `"messages":[{"messageId":1,"messageText":"Calibration images could not be sampled.","messagePayload":""}]`
- `"messages":[{"messageId":2,"messageText":"Max image has too few pixels withing distance limits.","messagePayload":""}]`
- `"messages":[{"messageId":3,"messageText":"Installation height is below the specified minimum","messagePayload":"2100"}]`
- `"messages":[{"messageId":4,"messageText":"Installation height is above the specified maximum","messagePayload":"2500"}]`
- `"messages":[{"messageId":5,"messageText":"The camera is not parallel to the floor","messagePayload":"5"}]`
- `"messages":[{"messageId":6,"messageText":"The camera is not parallel to the floor","messagePayload":"5"}]`
- `"messages":[{"messageId":7,"messageText":"Unknown error","messagePayload":""}]`
- `"messages":[{"messageId":8,"messageText":"3D-kamera communication error.","messagePayload":""}]`

And in some cases a combination of `messageId` with `3,4,5,6`, i.e.
```json
{
  "deviceId":"000ecd02c137",
  "visionStatus": {
    "calibrationLastUpdate":"2024-08-30T12:43:25Z",
    "messages": [
      {
        "messageId":4,
        "messageText":"Installation height is above the specified maximum",
        "messagePayload":"2500"
      },
      {
        "messageId":5,
        "messageText":"The camera is not parallel to the floor",
        "messagePayload":"5"
      },
      {
        "messageId":6,
        "messageText":"The camera is not parallel to the floor",
        "messagePayload":"5"
      }
    ]
  },
  "timestamp":1725021805
}
```

The following tables gives a description of all the different `messageId` and possible solutions.

| `"messageId"` | Description                                                                                                                            | Possible solution                                                                                                                                                                                                                                                                                                        | `"messagePayload"`                                             |
| ------------- | -------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | -------------------------------------------------------------- |
| `0`           | Calibration successful                                                                                                                 |                                                                                                                                                                                                                                                                                                                          | `""`                                                           |
| `1`           | Device could not sample sufficient number of images within 5 minutes                                                                   | Make sure nothing is preventing the camera from measuring the floor: <ol><li>Camera lens is clean</li><li>No cables, spiderweb, pigs, equipment, etc. is blocking the view to the floor</li><li>Camera orientation and/or height is out of specification</li><li>Camera installed too close to the trough/wall</li></ol> | `""`                                                           |
| `2`           | If less than 20% of the camera lens is between 1600 mm and 3000 mm                                                                     | Make sure nothing is preventing the camera from measuring the floor: <ol><li>Camera lens is clean</li><li>No cables, spiderweb, pigs, equipment, etc. is blocking the view to the floor</li><li>Camera orientation and/or height is out of specification</li><li>Camera installed too close to the trough/wall</li></ol> | `""`                                                           |
| `3`           | The measured height during calibration, is less than the required minimum which is 2200 mm                                             | Raise the camera so it reaches the specified height above the floor (2300 mm)                                                                                                                                                                                                                                            | The measures height above the floor in mm (str)                |
| `4`           | The measured height during calibration, is less than the required maximum which is 2400 mm                                             | Lower the camera so it reaches the specified height above the floor (2300 mm)                                                                                                                                                                                                                                            | The measures height above the floor in mm (str)                |
| `5`           | The camera, relative to the floor, is tilted along the axis parallel to the hinge more than the maximum allowed angle which is 4°      | Level the camera                                                                                                                                                                                                                                                                                                         | The measured angle between the floor and the tilted axis (str) |
| `6`           | The camera, relative to the floor, is tilted along the axis perpendicular to the hinge more than the maximum allowed angle which is 4° | Level the camera                                                                                                                                                                                                                                                                                                         | The measured angle between the floor and the tilted axis (str) |
| `7`           | Calibration failed for unknown reason                                                                                                  | Verify installation, power cycle the camera and try calibrating again                                                                                                                                                                                                                                                    | `""`                                                           |
| `8`           | Camera connection errors. This error is generated in situations where the connection to the internal 3D-camera fails                   | Try calibrating again. If it still does not work power off the iDOL 65, disconnect the internal USB cable (in both ends), reconnect the cable and power, and retry calibration                                                                                                                                           | `""`                                                           |

## Camera lens related messages

Messages related to camera lens status will have message property subject `VisionStatusMessage`.
The device will continuously monitor its camera lens and send a message if something has happened to change the status of `visionStatus.isDirty`.

```json
{
  "deviceId":"000ecd02c137",
  "visionStatus": {
    "isDirty":"Clean"
  },
  "timestamp":1725024229
}
```

`"isDirty"` has three states: `"Clean"`, `"Dirty"`, `"Very Dirty"`. Dirty in this case is defined as the device's ability to measure depth information from the images captured by the camera. Roughly speaking, if the depth value of a pixel is zero, we define that pixel as being dirty. When the device is turned on, its dirty state will always start in `"isDirty": "Clean"`. If prior to a reboot the device has reported any other state, i.e. `"Dirty"` or `"Very Dirty"`, a reboot of the device will trigger the device to report `"Clean"`. The device will report `"Very Dirty"` if the camera lens is above 80% dirty. Otherwise the device will report `"Dirty"` if it is above 10% dirty and isn't able to detect a sufficient number of pigs, and it will report `"Very Dirty"` if is above 30% and isn't able to detect a sufficient number of pigs.

# Data messages
## Messages from weight calculation

Messages related to weight data will have message property subject `data`.
Every 10 second the device will capture an image and process all the pigs found within that image and saves the result in an internal buffer, which is a 24 hour buffer of all the pigs detected and estimated. Every hour these pig data will be aggregated and sent in the following two json formats.

```json
{
  "id":"2c3f0d52-f532-456c-bac6-2230ca29dac6",
  "deviceId":"000ecd02c137",
  "sensorId":"000ecd02c137_Camera01_kg",
  "sensorName":"Camera01",
  "value":0.0,
  "type":"Weight",
  "unit":"kg",
  "timestamp":1724918880,
  "withinSpec":false,
  "count":0,
  "timespan":60,
  "countDelta":0
}
```

```json
{
  "id":"2fbdaec1-2da4-4628-8a84-e31f6fb76319",
  "deviceId":"000ecd02c137",
  "sensorId":"000ecd02c137_Camera01_kg",
  "sensorName":"Camera01",
  "value":19.85,
  "type":"Weight",
  "unit":"kg",
  "timestamp":1724918340,
  "withinSpec":false,
  "count":212,
  "minWeight":5.31,
  "maxWeight":33.16,
  "timespan":60,
  "sd":12.95,
  "countDelta":30
}
```

The first json object contains all the key/values that will be sent if the number of pigs detected for the past 24 hours is 0, i.e. the pen is empty. The second json object contains all the key/values of when the number of pigs detected for the past 24 hours is greater than 0. The following table gives a description for some of the keys in the json object. 

| Key          | Description                                                                                                                                                                    | Type   |
| ------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------ |
| `value`      | The average weight of the pen from all the weight data from the past 24 hours.                                                                                                 | float  |
| `timestamp`  | Unix timestamp in seconds.                                                                                                                                                     | int    |
| `withinSpec` | `false` if the number of weight samples is below 300 and `true` if it is above.                                                                                                | bool   |
| `Count`      | A count of all the weight data saved in an internal buffer from the past 24 hours.                                                                                             | int    |
| `CountDelta` | The number of pigs weighed during the last cycle, i.e. a count of number of pigs weighed in the past 1 hour.                                                                   | int    |
| `SD`         | Standard deviation of all the weight data from the past 24 hours. This will not be part of the payload if `count` is 0.                                                        | float? |
| `MinWeight`  | $Value - 2 \cdot SD$, this value will not be below the minimum weight value of all the weight data in the buffer. This will not be part of the payload if `count` is 0.        | float? |
| `MaxWeight`  | $Value + 2 \cdot SD$, This value will not be greater than the maximum weight value of all the weight data in the buffer. This will not be part of the payload if `count` is 0. | float? |

