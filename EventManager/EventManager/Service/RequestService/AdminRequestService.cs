using AutoMapper;
using EventManager.Utility;
using ModelHolder.Context;
using ModelHolder.Dto;
using ModelHolder.Enums;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.RequestService
{
    public class AdminRequestService : IAdminRequestService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly HelperMethods helperMethods;

        public AdminRequestService(EventManagerDbContext dbContext, IMapper mapper, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.helperMethods = helperMethods;
        }

        public async Task<List<Request>> GetEventPendingRequests(long userId, long eventId)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);

            return dbContext.Requests.Where(x=>x.EventId == eventId).Where(x=>x.Status.Equals(RequestStatus.PENDING)).ToList();
        }

        public async Task<Request> GetRequestById(long userId, long requestId)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);

            var request = dbContext.Requests.FirstOrDefault(x=>x.RequestId == requestId);
            if (request == null)
            {
                throw new NotFoundException($"Заявки с id {requestId} не найдено");
            }
            return request;
        }

        public async Task<bool> VerifyRequest(long userId, long requestId, bool verify)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);

            var request = await dbContext.Requests.FindAsync(requestId);
            if (request == null)
            {
                throw new NotFoundException($"Заявки с id {requestId} не найдено");
            }
            var requestEvent = await dbContext.Events.FindAsync(request.EventId);
            if (requestEvent == null)
            {
                throw new NotFoundException($"Мероприятия с id {request.EventId} не найдено");
            }
            var eventRequestCount = dbContext.Requests.Where(r => r.EventId == request.EventId).Where(r => r.Status.Equals(RequestStatus.ACCEPTED)).Count();
            if (eventRequestCount >= requestEvent.ParticipantLimit)
            {
                request.Status = RequestStatus.REFUSED;
                await dbContext.SaveChangesAsync();
                return false;
            }
            if (!request.Status.Equals(RequestStatus.PENDING))
            {
                throw new IncorrectRequestStatusException();
            }
            if (verify)
            {
                request.Status = RequestStatus.ACCEPTED;
            }
            else
            {
                request.Status = RequestStatus.REFUSED;
            }
            await dbContext.SaveChangesAsync();
            return verify;
        }
    }
}
