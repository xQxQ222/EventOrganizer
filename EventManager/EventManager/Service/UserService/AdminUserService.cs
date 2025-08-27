using AutoMapper;
using EventManager.Utility;
using ModelHolder.Context;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.UserService
{
    public class AdminUserService : IAdminUserService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly HelperMethods helperMethods;

        public AdminUserService(EventManagerDbContext dbContext, IMapper mapper, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.helperMethods = helperMethods;
        }

        public async Task<User> GetUserById(long requesterId, long userId)
        {
            helperMethods.VerifyUserExistence(requesterId);
            helperMethods.CheckAdminRights(requesterId);
            var userToFind = dbContext.Users.FirstOrDefault(x=>x.TelegramId == userId);
            if (userToFind == null)
            {
                throw new NotFoundException($"Пользователь с id {userId} не найден");
            }
            return userToFind;
        }

        public async Task<List<User>> GetUsers(long userId)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);
            return dbContext.Users.ToList();
        }
    }
}
