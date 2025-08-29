using AutoMapper;
using EventManager.Utility;
using ModelHolder.Context;
using ModelHolder.Dto;
using ModelHolder.Enums;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.RequestService
{
    public class UserRequestService : IUserRequestService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly HelperMethods helperMethods;

        public UserRequestService(EventManagerDbContext dbContext, IMapper mapper, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.helperMethods = helperMethods;
        }

        public async Task<Request> AddNewRequest(long userId, RequestDto requestDto)
        {
            helperMethods.VerifyUserExistence(userId);

            var requestEvent = await dbContext.Events.FindAsync(requestDto.EventId);
            if (requestEvent == null)
            {
                throw new NotFoundException($"Мероприятия с id {requestDto.EventId} не найдено");
            }

            var eventRequestCount = dbContext.Requests.Where(r=>r.EventId == requestDto.EventId).Where(r=>r.Status.Equals(RequestStatus.ACCEPTED)).Count();
            if (eventRequestCount >= requestEvent.ParticipantLimit)
            {
                throw new ParticipantsOverflowException(requestDto.EventId);
            }
            var request = mapper.Map<Request>(requestDto);
            request.Status = RequestStatus.PENDING;
            request.RequesterId = userId;
            request.Created = DateTime.UtcNow;

            await dbContext.Requests.AddAsync(request);
            await dbContext.SaveChangesAsync();

            return request;
        }

        public async Task<Request> CancelMyRequest(long userId, long requestId)
        {
            helperMethods.VerifyUserExistence(userId);

            var request = await dbContext.Requests.FindAsync(requestId);

            if (request == null)
            {
                throw new NotFoundException($"Заявки с id {requestId} не найдено");
            }

            if(request.RequesterId != userId)
            {
                throw new NotAnAuthorException($"Пользователь с id {userId} не является автором заявки с id {requestId}");
            }

            if (!request.Status.Equals(RequestStatus.PENDING))
            {
                throw new IncorrectRequestStatusException();
            }

            request.Status = RequestStatus.CANCELLED;
            await dbContext.SaveChangesAsync();
            return request;
        }

        public async Task<List<Request>> GetMyRequests(long userId)
        {
            helperMethods.VerifyUserExistence(userId);

            return dbContext.Requests.Where(x=>x.RequesterId == userId).Where(x=>x.Status.Equals(RequestStatus.PENDING)).ToList();
        }
    }
}
