using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityHolder.Dto
{
    [AutoConstructor]
    public partial class EventDto
    {
        public string Title { get; }

        public string? Description { get; }

        public long LocationId { get; }

        public DateTime CreatedOn { get; }

        public DateTime EventDate { get; }

        public bool? IsPaid { get; }

        public int ParticipantLimit { get; }

        public long CategoryId { get; }

        public short State { get; }

        public long InitiatorId { get; }
    }
}
