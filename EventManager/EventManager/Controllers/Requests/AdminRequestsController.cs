using EventManager.Service.RequestService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EventManager.Controllers.Requests
{
    [ApiController]
    [Route("/api/admin/requests")]
    public class AdminRequestsController : Controller
    {
        private readonly IAdminRequestService requestService;
        private readonly ILogger<AdminRequestsController> log;

        public AdminRequestsController(IAdminRequestService requestService, ILogger<AdminRequestsController> log)
        {
            this.requestService = requestService;
            this.log = log;
        }

        [HttpPatch("{requestId:long}/verify")]
        public async Task<bool> VerifyRequest([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long requestId, [FromQuery] [Required] bool verify)
        {
            log.LogInformation("PATCH /api/admin/requests/{}/verify?verify={}", requestId, verify);
            return await requestService.VerifyRequest(userId, requestId, verify);
        }

        [HttpGet("{requestId:long}")]
        public async Task<Request> GetRequestById([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long requestId)
        {
            log.LogInformation("GET /api/admin/requests/{}", requestId);
            return await requestService.GetRequestById(userId, requestId);
        }

        [HttpGet("event/{eventId:long}")]
        public async Task<List<Request>> GetEventPendingRequests([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long eventId)
        {
            log.LogInformation("GET /api/admin/requests/event/{}", eventId);
            return await requestService.GetEventPendingRequests(userId, eventId);
        }
    }
}
