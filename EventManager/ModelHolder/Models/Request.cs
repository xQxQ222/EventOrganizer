using ModelHolder.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelHolder.Models;

public partial class Request
{
    public long RequestId { get; set; }

    public long RequesterId { get; set; }

    public long EventId { get; set; }

    public RequestStatus Status { get; set; }

    public DateTime Created { get; set; }

    [JsonIgnore]
    public virtual Event Event { get; set; } = null!;

    [JsonIgnore]
    public virtual User Requester { get; set; } = null!;
}
