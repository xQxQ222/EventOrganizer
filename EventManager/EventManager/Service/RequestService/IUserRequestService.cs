using ModelHolder.Dto;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.RequestService
{
    public interface IUserRequestService
    {
        Task<List<Request>> GetMyRequests(long userId);

        Task<Request> CancelMyRequest(long userId, long requestId);

        Task<Request> AddNewRequest(long userId, RequestDto request);
    }
}
