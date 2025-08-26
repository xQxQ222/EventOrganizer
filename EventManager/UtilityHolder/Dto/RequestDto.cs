using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityHolder.Dto
{
    public partial class RequestDto
    {
        public long RequesterId { get; }

        public long EventId { get; }

        public short Status { get; }

        public DateTime Created { get; }
    }
}
