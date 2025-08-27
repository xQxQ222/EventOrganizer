using EventManager.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Dto;
using ModelHolder.Models;
using System.Threading.Tasks;

namespace EventManager.Controllers.Users
{
    [ApiController]
    [Route("api/users")]
    public class PrivateUserController : Controller
    {

        private readonly IPrivateUserService userService;
        private ILogger<PrivateUserController> log;

        public PrivateUserController(IPrivateUserService userService, ILogger<PrivateUserController> log)
        {
            this.userService = userService;
            this.log = log;
        }

        [HttpPost]
        public async Task<User> RegisterNewUser([FromHeader(Name = "X-User-Id")] long userId, [FromBody] UserDto userDto)
        {
            log.LogInformation("POST api/users userId: {} с телом: {}", userId, userDto);
            return await userService.RegisterUser(userId, userDto);
        }

        [HttpPatch]
        public async Task<User> UpdateUser([FromHeader(Name = "X-User-Id")] long userId, [FromBody] UserDto userDto)
        {
            log.LogInformation("PATCH /api/users userId: {} с телом: {}", userId, userDto);
            return await userService.UpdateUser(userId, userDto);
        }

        [HttpGet]
        public async Task<User> GetProfile([FromHeader(Name = "X-User-Id")] long userId)
        {
            log.LogInformation("GET /api/users userId: {}", userId);
            return await userService.GetProfile(userId);
        }
    }
}
