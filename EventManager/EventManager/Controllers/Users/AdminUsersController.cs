using EventManager.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityHolder.Dto;

namespace EventManager.Controllers.Users
{
    [ApiController]
    [Route("/api/admin/users")]
    public class AdminUsersController : Controller
    {
        private readonly ILogger<AdminUsersController> log;
        private readonly IUserService userService;

        public AdminUsersController(ILogger<AdminUsersController> log, IUserService userService)
        {
            this.log = log;
            this.userService = userService;
        }

        [HttpGet("{userId:long}")]
        public async Task<User> GetUserById(long userId)
        {
            log.LogInformation("GET /api/admin/users/{}", userId);
            return await userService.GetUserById(userId);
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            log.LogInformation("GET /api/admin/users");
            return await userService.GetUsers();
        }

        [HttpPost]
        public async Task<User> RegisterNewUser(long userId, UserDto userDto)
        {
            log.LogInformation("POST /api/admin/users, userId: {} с телом: {}", userId, userDto);
            return await userService.RegisterUser(userId, userDto);
        }

        [HttpPatch]
        public async Task<User> UpdateUser(long userId, UserDto userDto)
        {
            log.LogInformation("PATCH /api/admin/users, userId: {} с телом: {}", userId, userDto);
            return await userService.UpdateUser(userId, userDto);
        }
    }
}
