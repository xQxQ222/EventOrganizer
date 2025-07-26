using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ModelHolder.Models
{
    /// <summary>
    /// Описание события
    /// </summary>
    public class Event
    {
        public int Id { get; set; }
        [Required, NotNull]
        public string Name { get; set; }
        [AllowNull]
        public string Description { get; set; }

        [Required, NotNull]
        public User Initiator { get; set; }

        [Required, NotNull]
        public EventCategory Category { get; set; }

        [Required, NotNull]
        public Location Location { get; set; }

        [DefaultValue(false)]
        public bool IsTicketed { get; set; }

        [DefaultValue(10)]
        public int ParticipantLimit { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime EventDate { get; set; }

        [DefaultValue(false)]
        public bool IsPaid {  get; set; }
    }
}
