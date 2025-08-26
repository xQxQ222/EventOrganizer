using ModelHolder.Models;
using System.Collections.Generic;
using UtilityHolder.Dto;

namespace EventManager.Service.UserService
{
    public class UserService : IUserService
    {
        public User GetUser(long userId)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetUsers()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterUser(UserDto userDto)
        {
            throw new System.NotImplementedException();
        }

        public User UpdateUser(long userId, UserDto userDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
