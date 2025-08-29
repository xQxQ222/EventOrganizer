using EventManager.Service.Images;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Controllers.Images
{
    [ApiController]
    [Route("/api/admin/images")]
    public class AdminImagesController : Controller
    {
        private readonly IAdminImageService imageService;
        private readonly ILogger<AdminImagesController> log;

        public AdminImagesController(IAdminImageService imageService, ILogger<AdminImagesController> log)
        {
            this.imageService = imageService;
            this.log = log;
        }

        [HttpPost("event/{eventId:long}")]
        public async Task<List<Image>> PostImages([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long eventId, [FromBody] List<string> imagesPath)
        {
            log.LogInformation("POST /api/admin/images/event/{} с телом: {}", eventId, imagesPath);
            return await imageService.PostImages(userId, eventId, imagesPath);
        }

        [HttpDelete]
        public async Task DeleteImages([FromHeader(Name = "X-User-Id")] long userId, [FromBody] List<long> imagesIds)
        {
            log.LogInformation("DELETE /api/admin/images с телом: {}", imagesIds);
            await imageService.DeleteImages(userId, imagesIds);
        }
    }
}
