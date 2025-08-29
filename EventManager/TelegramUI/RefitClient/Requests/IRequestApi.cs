using ModelHolder.Dto;
using ModelHolder.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramUI.RefitClient.Requests
{
    public interface IRequestApi
    {
        [Patch("/api/admin/requests/{requestId}")]
        Task<bool> VerifyRequest([Header("X-User-Id")] long userId, long requestId, [Query] bool verify);

        [Get("/api/admin/requests/{requestId}")]
        Task<Request> GetRequestById([Header("X-User-Id")] long userId, long requestId);

        [Get("/api/admin/requests/event/{eventId}")]
        Task<List<Request>> GetEventPendingRequests([Header("X-User-Id")] long userId, long eventId);

        [Get("/api/requests/my")]
        Task<List<Request>> GetMyRequests([Header("X-User-Id")] long userId);

        [Patch("/api/requests/{requestId}/cancel")]
        Task<Request> CancelMyRequest([Header("X-User-Id")] long userId, long requestId);

        [Post("/api/requests")]
        Task<Request> PostNewRequest([Header("X-User-Id")] long userId, [Body] RequestDto requestDto);
    }
}
