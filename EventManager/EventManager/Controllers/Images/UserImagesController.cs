using EventManager.Service.Images;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Controllers.Images
{
    [ApiController]
    [Route("/api/images")]
    public class UserImagesController : Controller
    {
        private readonly IUserImagesService imagesService;
        private readonly ILogger<UserImagesController> log;

        public UserImagesController(IUserImagesService imagesService, ILogger<UserImagesController> log)
        {
            this.imagesService = imagesService;
            this.log = log;
        }

        [HttpGet("event/{eventId:long}")]
        public async Task<List<Image>> GetEventImages([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long eventId)
        {
            log.LogInformation("GET /api/images/event/{}", eventId);
            return await imagesService.GetEventImages(userId, eventId);
        }
    }
}
