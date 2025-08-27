using AutoMapper;
using EventManager.Exceptions;
using Microsoft.EntityFrameworkCore;
using ModelHolder.Context;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtilityHolder.Dto;

namespace EventManager.Service.UserService
{
    public class UserService : IUserService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly IMapper mapper;

        public UserService(EventManagerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<User> GetUserById(long userId)
        {
            var user = dbContext.Users.FirstOrDefault(x=>x.TelegramId == userId);
            if (user == null)
            {
                throw new NotFoundException($"Пользователь с id {userId} не найден");
            }
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return dbContext.Users.ToList();
        }

        public async Task<User> RegisterUser(long userId, UserDto userDto)
        {
            if(dbContext.Users.FirstOrDefault(x=>x.TelegramId == userId)  != null)
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
            var user = await dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"Пользователь с id {userId} не найден");
            }
            if (userDto.Email != null)
            {
                user.Email = userDto.Email;
            }
            await dbContext.SaveChangesAsync();
            return user;
        }
    }
}
