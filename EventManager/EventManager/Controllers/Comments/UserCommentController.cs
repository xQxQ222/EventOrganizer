using EventManager.Service.CommentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Dto;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Controllers.Comments
{
    [ApiController]
    [Route("/api/comments")]
    public class UserCommentController : Controller
    {
        private readonly IUserCommentService commentService;
        private readonly ILogger<UserCommentController> log;

        public UserCommentController(IUserCommentService commentService, ILogger<UserCommentController> log)
        {
            this.commentService = commentService;
            this.log = log;
        }

        [HttpGet("my")]
        public Task<List<Comment>> GetMyComments([FromHeader(Name = "X-User-Id")] long userId)
        {
            log.LogInformation("GET /api/comments/my");
            return commentService.GetMyComments(userId);
        }

        [HttpPost]
        public Task<Comment> PostNewComment([FromHeader(Name = "X-User-Id")] long userId, [FromBody] CommentDto commentDto)
        {
            log.LogInformation("POST /api/comments");
            return commentService.PostNewComment(userId, commentDto);
        }

        [HttpPatch("my/{commentId:long}")]
        public Task<Comment> UpdateMyComment([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long commentId, [FromBody] CommentUpdateDto commentDto)
        {
            log.LogInformation("PATCH /api/comments/my/{}", commentId);
            return commentService.UpdateMyComment(userId, commentId, commentDto);
        }

        [HttpGet("event/{eventId:long}")]
        public Task<List<Comment>> GetEventComments([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long eventId)
        {
            log.LogInformation("GET /api/comments/event/{}", eventId);
            return commentService.GetEventComments(userId, eventId);
        }

    }
}
