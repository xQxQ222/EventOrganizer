using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UtilityHolder.Dto
{
    [AutoConstructor]
    public class UserDto
    {
        public long TelegramId { get; }

        [MinLength(5)]
        public string? Email { get; }

        [NotNull]
        public short RoleId { get; }
    }
}
