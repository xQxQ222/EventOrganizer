using ModelHolder.Dto;
using ModelHolder.Models;
using System;
using System.Threading.Tasks;

namespace EventManager.Service.EventService
{
    public interface IAdminEventService
    {
        Task DeleteEventById(long userId, long eventId);

        Task<Event> PostNewEvent(long userId, EventDto eventDto);

        Task<Event> UpdateEvent(long userId, long eventId, EventUpdateDto eventDto);

        Task<Event> RescheduleEvent(long userId, long eventId, DateTime newEventDate);
    }
}
