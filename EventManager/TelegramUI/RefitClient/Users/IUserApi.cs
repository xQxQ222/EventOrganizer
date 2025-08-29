using ModelHolder.Dto;
using ModelHolder.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramUI.RefitClient.Users
{
    public interface IUserApi
    {
        [Get("/api/admin/users/{userToFindId}")]
        Task<User> GetUserById([Header("X-User-Id")] long userId, long userToFindId);

        [Get("/api/admin/users")]
        Task<List<User>> GetAllUsers([Header("X-User-Id")] long userId);

        [Post("/api/users")]
        Task<User> RegisterNewUser([Header("X-User-Id")] long userId, [Body] UserDto userDto);

        [Patch("/api/users")]
        Task<User> UpdateUser([Header("X-User-Id")] long userId, [Body] UserDto userDto);

        [Get("/api/users/profile")]
        Task<User> GetProfile([Header("X-User-Id")] long userId);
    }
}
