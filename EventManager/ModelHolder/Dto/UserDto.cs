using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ModelHolder.Dto
{
    [AutoConstructor]
    public partial class UserDto
    {

        [MinLength(5)]
        public string? Email { get; }

    }
}
