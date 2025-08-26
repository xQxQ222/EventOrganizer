using System;
using System.Collections.Generic;

namespace ModelHolder.Models;

public partial class User
{
    public long Id { get; set; }

    public long TelegramId { get; set; }

    public string? Email { get; set; }

    public short RoleId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual Role Role { get; set; } = null!;
}
