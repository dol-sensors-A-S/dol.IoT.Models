using System.Collections.Generic;

namespace dol.IoT.Models.Public.DeviceApi;

public class HttpValidationProblemDetails
{
    public required string Type { get; set; }
    public required string Title { get; set; }
    public int? Status { get; set; }
    public required string Detail { get; set; }
    public required string Instance { get; set; }
    public IDictionary<string, ICollection<string>>? Errors { get; set; }
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}