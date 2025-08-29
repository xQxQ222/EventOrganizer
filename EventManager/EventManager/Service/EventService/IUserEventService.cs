using ModelHolder.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.EventService
{
    public interface IUserEventService
    {
        Task<Event> GetEventById(long userId, long eventId);

        Task<List<Event>> GetMyEvents(long userId);

        Task<List<Event>> GetEventByCategory(long userId, long categoryId);

        Task<List<Event>> GetEventsByDate(long userId, DateTime? from, DateTime? to);
    }
}
