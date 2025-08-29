using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelHolder.Models;

public partial class Image
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public string Path { get; set; } = null!;

    [JsonIgnore]
    public virtual Event Event { get; set; } = null!;
}
