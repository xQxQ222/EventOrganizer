using ModelHolder.Enums;
using System;
using System.Collections.Generic;

namespace ModelHolder.Models;

public partial class Request
{
    public long RequestId { get; set; }

    public long RequesterId { get; set; }

    public long EventId { get; set; }

    public RequestStatus Status { get; set; }

    public DateTime Created { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User Requester { get; set; } = null!;
}
