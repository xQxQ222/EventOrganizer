using System;
using System.Collections.Generic;

namespace ModelHolder.Models;

public partial class Category
{
    public long CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
