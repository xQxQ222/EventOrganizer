using EventManager.Service.RequestService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Dto;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Controllers.Requests
{
    [ApiController]
    [Route("/api/requests")]
    public class UserRequestsController : Controller
    {
        private readonly IUserRequestService requestService;
        private readonly ILogger<UserRequestsController> log;

        public UserRequestsController(IUserRequestService requestService, ILogger<UserRequestsController> log)
        {
            this.requestService = requestService;
            this.log = log;
        }

        [HttpGet("/my")]
        public async Task<List<Request>> GetMyRequests([FromHeader(Name = "X-User-Id")] long userId)
        {
            log.LogInformation("GET /api/requests/my");
            return await requestService.GetMyRequests(userId);
        }

        [HttpPatch("{requestId:long}/cancel")]
        public async Task<Request> CancelMyRequest([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long requestId)
        {
            log.LogInformation("PATCH /api/requests/{}/cancel", requestId);
            return await requestService.CancelMyRequest(userId, requestId);
        }

        [HttpPost]
        public async Task<Request> PostNewRequest([FromHeader(Name = "X-User-Id")] long userId, [FromBody] RequestDto requestDto)
        {
            log.LogInformation("POST /api/requests с телом: {}", requestDto);
            return await requestService.AddNewRequest(userId, requestDto);
        }
       
    }
}
