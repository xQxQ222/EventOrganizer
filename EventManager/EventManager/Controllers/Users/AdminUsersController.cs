using EventManager.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Dto;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Controllers.Users
{
    [ApiController]
    [Route("/api/admin/users")]
    public class AdminUsersController : Controller
    {
        private readonly ILogger<AdminUsersController> log;
        private readonly IAdminUserService userService;

        public AdminUsersController(ILogger<AdminUsersController> log, IAdminUserService userService)
        {
            this.log = log;
            this.userService = userService;
        }

        [HttpGet("{userToFindId:long}")]
        public async Task<User> GetUserById([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long userToFindId)
        {
            log.LogInformation("GET /api/admin/users/{}", userId);
            return await userService.GetUserById(userId, userToFindId);
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers([FromHeader(Name = "X-User-Id")] long userId)
        {
            log.LogInformation("GET /api/admin/users");
            return await userService.GetUsers(userId);
        }
    }
}
