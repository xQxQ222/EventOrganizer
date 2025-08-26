using System;
using System.Collections.Generic;

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

    public virtual User Author { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
