using ModelHolder.Models;
using System.Collections.Generic;
using UtilityHolder.Dto;

namespace EventManager.Service.UserService
{
    public interface IUserService
    {
        void RegisterUser(UserDto userDto);

        User UpdateUser(long userId, UserDto userDto);

        User GetUser(long userId);

        User GetUserByEmail(string email);

        List<User> GetUsers();
    }
}
