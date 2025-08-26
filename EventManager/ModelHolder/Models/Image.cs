using System;
using System.Collections.Generic;

namespace ModelHolder.Models;

public partial class Image
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public string Path { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
