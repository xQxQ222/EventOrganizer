using EventManager.Service.EventService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Dto;
using ModelHolder.Models;
using System;
using System.Threading.Tasks;

namespace EventManager.Controllers.Events
{
    [ApiController]
    [Route("/api/admin/events")]
    public class AdminEventController : Controller
    {
        private readonly ILogger<AdminEventController> log;
        private readonly IAdminEventService eventService;

        public AdminEventController(ILogger<AdminEventController> log, IAdminEventService eventService)
        {
            this.log = log;
            this.eventService = eventService;
        }

        [HttpPatch("{eventId:long}")]
        public async Task<Event> UpdateEvent([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long eventId, [FromBody] EventUpdateDto eventToUpdate)
        {
            log.LogInformation("PATCH /api/admin/events/{} с телом: {}", eventId, eventToUpdate);
            return await eventService.UpdateEvent(userId, eventId, eventToUpdate);
        }

        [HttpPost]
        public async Task<Event> AddNewEvent([FromHeader(Name = "X-User-Id")] long userId, EventDto eventDto)
        {
            log.LogInformation("POST /api/admin/events с телом: {}", eventDto);
            return await eventService.PostNewEvent(userId, eventDto);
        }

        [HttpDelete]
        public async Task DeleteEventById([FromHeader(Name = "X-User-Id")] long userId, long eventId)
        {
            log.LogInformation("DELETE /api/admin/events eventId: {}", eventId);
            await eventService.DeleteEventById(userId, eventId);
        }

        [HttpPatch("{eventId:long}/reschedule")]
        public async Task<Event> RescheduleEvent([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long eventId, [FromBody] DateTime newEventDate)
        {
            log.LogInformation("PATCH /api/admin/events/{}/reschedule с телом: {}", eventId, newEventDate);
            return await eventService.RescheduleEvent(userId, eventId, newEventDate);
        }
    }
}
