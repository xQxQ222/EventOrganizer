using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityHolder.Dto;

namespace EventManager.Service.UserService
{
    public interface IUserService
    {
        Task<User> RegisterUser(long userId, UserDto userDto);

        Task<User> UpdateUser(long userId, UserDto userDto);

        Task<User> GetUserById(long userId);

        Task<List<User>> GetUsers();
    }
}
