using EventManager.Service.CommentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Controllers.Comments
{
    [ApiController]
    [Route("/api/admin/comments")]
    public class AdminCommentController : Controller
    {
        private readonly IAdminCommentService commentService;
        private readonly ILogger<AdminCommentController> log;

        public AdminCommentController(IAdminCommentService commentService, ILogger<AdminCommentController> log)
        {
            this.commentService = commentService;
            this.log = log;
        }

        [HttpDelete("{commentId:long}")]
        public async Task DeleteCommentById([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long commentId)
        {
            log.LogInformation("DELETE /api/admin/comments/{}", commentId);
            await commentService.DeleteComment(userId, commentId);
        }

        [HttpGet("from/{commentatorId:long}")]
        public async Task<List<Comment>> GetUserComments([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long commentatorId)
        {
            log.LogInformation("GET /api/admin/comments/from/{}", commentatorId);
            return await commentService.GetUserComments(userId, commentatorId);
        }

        [HttpGet("{commentId:long}")]
        public async Task<Comment> GetCommentById([FromHeader(Name = "X-User-Id")] long userId, [FromRoute] long commentId)
        {
            log.LogInformation("GET /api/admin/comments/{}", commentId);
            return await commentService.GetCommentById(userId, commentId);
        }
    }
}
