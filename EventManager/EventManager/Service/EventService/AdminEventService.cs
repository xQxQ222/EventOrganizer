using AutoMapper;
using EventManager.Utility;
using ModelHolder.Context;
using ModelHolder.Dto;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System;
using System.Threading.Tasks;

namespace EventManager.Service.EventService
{
    public class AdminEventService : IAdminEventService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly HelperMethods helperMethods;
        private readonly IMapper mapper;

        public AdminEventService(EventManagerDbContext dbContext, HelperMethods helperMethods, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.helperMethods = helperMethods;
            this.mapper = mapper;
        }

        public async Task DeleteEventById(long userId, long eventId)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(eventId);

            var eventFromDb = await dbContext.Events.FindAsync(eventId);
            if (eventFromDb == null)
            {
                throw new NotFoundException($"Мероприятие с id {eventId} не найдено");
            }
            dbContext.Events.Remove(eventFromDb);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Event> PostNewEvent(long userId, EventDto eventDto)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);

            if (eventDto.EventDate < DateTime.Today.AddDays(1))
            {
                throw new IncorrectEventDate();
            }

            var category = dbContext.Categories.Find(eventDto.CategoryId);
            if(category == null)
            {
                throw new NotFoundException($"Категория с id {eventDto.CategoryId} не найдена");
            }

            Event eventToAdd = mapper.Map<Event>(eventDto);
            eventToAdd.InitiatorId = userId;
            eventToAdd.CreatedOn = DateTime.UtcNow;

            await dbContext.Events.AddAsync(eventToAdd);
            await dbContext.SaveChangesAsync();

            return eventToAdd;
        }

        public async Task<Event> RescheduleEvent(long userId, long eventId, DateTime newEventDate)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);
            
            var eventFromDb = dbContext.Events.Find(eventId);
            if(eventFromDb == null)
            {
                throw new NotFoundException($"Мероприятие с id {eventId} не найдено");
            }
            if(eventFromDb.EventDate.AddHours(2) > newEventDate)
            {
                throw new PastEventException();
            }
            if (newEventDate < DateTime.Today.AddDays(1))
            {
                throw new IncorrectEventDate();
            }
            eventFromDb.EventDate = newEventDate;
            await dbContext.SaveChangesAsync();

            return eventFromDb;
        }

        public async Task<Event> UpdateEvent(long userId, long eventId, EventUpdateDto eventDto)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);

            var eventFromDb = dbContext.Events.Find(eventId);
            if (eventFromDb == null)
            {
                throw new NotFoundException($"Мероприятие с id {eventId} не найдено");
            }

            var category = dbContext.Categories.Find(eventDto.CategoryId);
            if(category == null)
            {
                throw new NotFoundException($"Категория с id {eventDto.CategoryId} не найдена");
            }

            eventFromDb.CategoryId = eventDto.CategoryId;
            if (eventDto.Title != null)
            {
                eventFromDb.Title = eventDto.Title;
            }
            if (eventDto.Description != null)
            {
                eventFromDb.Description = eventDto.Description;
            }
            if (eventDto.Location != null)
            {
                eventFromDb.Location = eventDto.Location;
            }
            eventFromDb.ParticipantLimit = eventDto.ParticipantLimit;
            eventFromDb.IsPaid = eventDto.IsPaid;
            if(eventDto.Location != null)
            {
                eventFromDb.Location = eventDto.Location;
            }
            await dbContext.SaveChangesAsync();
            return eventFromDb;
        }
    }
}
