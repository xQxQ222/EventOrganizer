using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.RequestService
{
    public interface IAdminRequestService
    {
        Task<bool> VerifyRequest(long userId, long requestId, bool verify);

        Task<Request> GetRequestById(long userId, long requestId);

        Task<List<Request>> GetEventPendingRequests(long userId, long eventId);
    }
}
