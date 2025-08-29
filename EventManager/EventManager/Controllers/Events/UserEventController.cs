using EventManager.Service.EventService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Controllers.Events
{
    [ApiController]
    [Route("/api/events")]
    public class UserEventController : Controller
    {
        private readonly ILogger<UserEventController> log;
        private IUserEventService eventService;

        public UserEventController(ILogger<UserEventController> log, IUserEventService eventService)
        {
            this.log = log;
            this.eventService = eventService;
        }

        [HttpGet("{eventId:long}")]
        public async Task<Event> GetEventById([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long eventId)
        {
            log.LogInformation("GET /api/events/{}", eventId);
            return await eventService.GetEventById(userId, eventId);
        }

        [HttpGet("category/{categoryId:long}")]
        public async Task<List<Event>> GetEventsByCategory([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long categoryId)
        {
            log.LogInformation("GET /api/events/category/{}", categoryId);
            return await eventService.GetEventByCategory(userId, categoryId);
        }

        [HttpGet("my")]
        public async Task<List<Event>> GetMyEvents([FromHeader(Name = "X-User-Id")] long userId)
        {
            log.LogInformation("GET /api/events/my");
            return await eventService.GetMyEvents(userId);
        }

        [HttpGet("date")]
        public async Task<List<Event>> GetEventsByDate([FromHeader(Name = "X-User-Id")] long userId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            log.LogInformation("GET /api/events/date?from={}&to={}", from, to);
            return await eventService.GetEventsByDate(userId, from, to);
        }

    }
}
