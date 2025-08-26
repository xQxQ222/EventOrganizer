using System;
using System.Collections.Generic;

namespace ModelHolder.Models;

public partial class Location
{
    public long Id { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
