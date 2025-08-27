using ModelHolder.Dto;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.UserService
{
    public interface IAdminUserService
    {
        Task<User> GetUserById(long userId, long userToFindId);

        Task<List<User>> GetUsers(long userId);
    }
}
