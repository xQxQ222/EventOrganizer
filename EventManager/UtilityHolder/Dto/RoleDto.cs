using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityHolder.Dto
{
    [AutoConstructor]
    public partial class RoleDto
    {
        public string RoleName { get; }

        public string? Description { get; }
    }
}
