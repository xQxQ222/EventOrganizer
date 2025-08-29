using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    public long InitiatorId { get; set; }


    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;


    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [JsonIgnore]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    [JsonIgnore]
    public virtual User Initiator { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
