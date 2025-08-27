using ModelHolder.Models;
using System;

namespace ModelHolder.Dto
{
    [AutoConstructor]
    public partial class EventUpdateDto
    {
        public string Title { get; }

        public string? Description { get; }

        public Location Location { get; }

        public bool? IsPaid { get; }

        public int ParticipantLimit { get; }

        public long CategoryId { get; }
    }
}
