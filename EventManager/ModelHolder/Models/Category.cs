using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelHolder.Models;

public partial class Category
{
    public long CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
