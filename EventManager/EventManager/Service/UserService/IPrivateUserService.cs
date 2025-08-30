using Microsoft.AspNetCore.Mvc;
using ModelHolder.Dto;
using ModelHolder.Models;
using System.Threading.Tasks;

namespace EventManager.Service.UserService
{
    public interface IPrivateUserService
    {
        Task<User> RegisterUser(long userId, UserDto userDto);

        Task<User> UpdateUser(long userId, UserDto userDto);

        Task<User> GetProfile(long userId);

        Task<bool> CheckUserExistence(long userId);
    }
}
