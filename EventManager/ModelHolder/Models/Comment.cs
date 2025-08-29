using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelHolder.Models;

public partial class Comment
{
    public long Id { get; set; }

    public long AuthorId { get; set; }

    public long EventId { get; set; }

    public DateTime Created { get; set; }

    public bool IsPositive { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    [JsonIgnore]
    public virtual User Author { get; set; } = null!;

    [JsonIgnore]
    public virtual Event Event { get; set; } = null!;
}
