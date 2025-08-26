using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityHolder.Dto
{
    [AutoConstructor]
    public partial class CommentDto
    {
        public long AuthorId { get; }

        public long EventId { get; }

        public DateTime Created { get; }

        public bool IsPositive { get; }

        public string Title { get; }

        public string Description { get; }
    }
}
