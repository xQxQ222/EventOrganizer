using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Dto
{
    [AutoConstructor]
    public partial class CommentUpdateDto
    {
        public bool IsPositive { get; }

        public string Title { get; }

        public string Description { get; }
    }
}
