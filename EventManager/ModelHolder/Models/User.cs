using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelHolder.Models;

public partial class User
{
    public long TelegramId { get; set; }

    public string? Email { get; set; }

    public short RoleId { get; set; }

    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [JsonIgnore]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [JsonIgnore]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    [JsonIgnore]
    public virtual Role Role { get; set; } = null!;
}
