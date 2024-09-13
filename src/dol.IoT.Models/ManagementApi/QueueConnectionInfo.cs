namespace dol.IoT.Models.Public.ManagementApi;

public record QueueConnectionInfoResponse(
    string DataQueueConnection,
    string DataQueueName,
    bool DataUsingPrimaryKey,
    string StatusQueueConnection,
    string StatusQueueName,
    bool StatusUsingPrimaryKey,
    long DataQueueMessageCount,
    long DataQueueDeadLetterCount,
    long StatusQueueMessageCount,
    long StatusQueueDeadLetterCount);