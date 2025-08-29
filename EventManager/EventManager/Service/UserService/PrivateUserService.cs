using AutoMapper;
using EventManager.Kafka;
using EventManager.Utility;
using Microsoft.EntityFrameworkCore;
using ModelHolder.Context;
using ModelHolder.Dto;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.UserService
{
    public class PrivateUserService : IPrivateUserService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly HelperMethods helperMethods;

        public PrivateUserService(EventManagerDbContext dbContext, IMapper mapper, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.helperMethods = helperMethods;
        }

        public async Task<User> GetProfile(long userId)
        {
            helperMethods.VerifyUserExistence(userId);
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.TelegramId == userId);
            helperMethods.VerifyUserExistence(userId);
            return user;
        }

        public async Task<User> RegisterUser(long userId, UserDto userDto)
        {
            if (dbContext.Users.FirstOrDefault(x => x.TelegramId == userId) != null)
            {
                throw new AlreadyRegisteredException($"Пользователь с id {userId} уже зарегистрирован");
            }

            var user = mapper.Map<User>(userDto);
            user.TelegramId = userId;
            user.RoleId = 2;

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(long userId, UserDto userDto)
        {
            helperMethods.VerifyUserExistence(userId);
            var user = dbContext.Users.FirstOrDefault(x => x.TelegramId == userId);
            if (userDto.Email != null)
            {
                user.Email = userDto.Email;
            }
            await dbContext.SaveChangesAsync();
            return user;
        }


    }
}
