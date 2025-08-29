using ModelHolder.Dto;
using ModelHolder.Models;
using Refit;

namespace TelegramUI.RefitClient.Events
{
    public interface IEventApi
    {
        [Patch("/api/admin/events/{eventId}")]
        Task<Event> UpdateEvent([Header("X-User-Id")] long userId, long eventId, [Body] EventUpdateDto eventToUpdate);

        [Post("/api/admin/events")]
        Task<Event> AddNewEvent([Header("X-User-Id")] long userId, [Body] EventDto eventDto);

        [Delete("/api/admin/events/{eventId}")]
        Task DeleteEventById([Header("X-User-Id")] long userId, long eventId);

        [Patch("/api/admin/events/{eventId}/reschedule")]
        Task<Event> RescheduleEvent([Header("X-User-Id")] long userId, long eventId, [Body] DateTime newEventDate);

        [Get("/api/events/{eventId}")]
        Task<Event> GetEventById([Header("X-User-Id")] long userId, long eventId);

        [Get("/api/events/category/{categoryId}")]
        Task<List<Event>> GetEventsByCategory([Header("X-User-Id")] long userId, long categoryId);

        [Get("/api/events/my")]
        Task<List<Event>> GetMyEvents([Header("X-User-Id")] long userId);

        [Get("/api/events/date")]
        Task<List<Event>> GetEventsByDate([Header("X-User-Id")] long userId, [Query] DateTime? from, [Query] DateTime? to);

        [Get("/api/events/requests/accepted")]
        Task<List<Event>> GetMyAcceptedRequestEvents([Header("X-User-Id")] long userId);
    }
}
