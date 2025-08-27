using AutoMapper;
using EventManager.Utility;
using Microsoft.EntityFrameworkCore;
using ModelHolder.Context;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.EventService
{
    public class UserEventService : IUserEventService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly HelperMethods helperMethods;

        public UserEventService(EventManagerDbContext dbContext, IMapper mapper, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.helperMethods = helperMethods;
        }

        public async Task<List<Event>> GetEventByCategory(long userId, short categoryId)
        {
            helperMethods.VerifyUserExistence(userId);

            var category = dbContext.Categories.Find(categoryId);
            if (category == null)
            {
                throw new NotFoundException($"Категория с id {categoryId} не найдена");
            }

            return dbContext.Events.Where(x => x.CategoryId == categoryId).Where(x=>x.EventDate >= DateTime.Today).ToList();
        }

        public async Task<Event> GetEventById(long userId, long eventId)
        {
            helperMethods.VerifyUserExistence(userId);

            var eventFromDb = await dbContext.Events.FindAsync(eventId);
            if (eventFromDb == null)
            {
                throw new NotFoundException($"Мероприятия с id {eventId} не найдено");
            }
            return eventFromDb;
        }

        public async Task<List<Event>> GetEventsByDate(long userId, DateTime? from, DateTime? to)
        {
            helperMethods.VerifyUserExistence(userId);

            IQueryable<Event> query = dbContext.Events;

            if (from.HasValue)
            {
                query = query.Where(e => e.EventDate >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(e => e.EventDate <= to.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Event>> GetMyEvents(long userId)
        {
            helperMethods.VerifyUserExistence(userId);

            return dbContext.Events.Where(x=>x.InitiatorId == userId).Where(x => x.EventDate >= DateTime.Today).ToList();
        }
    }
}
