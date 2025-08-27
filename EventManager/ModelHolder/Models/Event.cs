using System;
using System.Collections.Generic;

namespace ModelHolder.Models;

public partial class Event
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public Location Location { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime EventDate { get; set; }

    public bool? IsPaid { get; set; }

    public int ParticipantLimit { get; set; }

    public long CategoryId { get; set; }

    public short State { get; set; }

    public long InitiatorId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual User Initiator { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
