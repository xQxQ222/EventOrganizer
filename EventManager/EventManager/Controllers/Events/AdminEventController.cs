using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Dto;
using ModelHolder.Models;
using UtilityHolder.Dto;

namespace EventManager.Controllers.Events
{
    [ApiController]
    [Route("/api/admin/events")]
    public class AdminEventController : Controller
    {
        private readonly ILogger<AdminEventController> logger;

        public AdminEventController(ILogger<AdminEventController> logger)
        {
            this.logger = logger;
        }

        [HttpPatch]
        public Event UpdateEvent(long id, EventDto eventToUpdate)
        {
            logger.LogInformation("Пришел PATCH запрос /api/admin/events с телом: {}", eventToUpdate);
            return null;
        }
    }
}
