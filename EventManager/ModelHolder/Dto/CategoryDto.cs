using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Dto
{
    [AutoConstructor]
    public partial class CategoryDto
    {
        public string CategoryName { get; }
    }
}
