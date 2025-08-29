using ModelHolder.Dto;
using ModelHolder.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramUI.RefitClient.Comments
{
    public interface ICommentsApi
    {
        [Delete("/api/admin/comments/{commentId}")]
        Task DeleteCommentById([Header("X-User-Id")] long userId, long commentId);

        [Get("/api/admin/comments/from/{commentatorId}")]
        Task<List<Comment>> GetUserComments([Header("X-User-Id")] long userId, long commentatorId);

        [Get("/api/admin/comments/{commentId}")]
        Task<Comment> GetCommentById([Header("X-User-Id")] long userId, long commentId);

        [Get("/api/comments/my")]
        Task<List<Comment>> GetMyComments([Header("X-User-Id")] long userId);

        [Post("/api/comments")]
        Task<Comment> PostNewComment([Header("X-User-Id")] long userId, [Body] CommentDto commentDto);

        [Patch("/api/comments/my/{commentId}")]
        Task<Comment> UpdateMyComment([Header("X-User-Id")] long userId, long commentId, [Body] CommentUpdateDto commentDto);

        [Get("/api/comments/event/{eventId}")]
        Task<List<Comment>> GetEventComments([Header("X-User-Id")] long userId, long eventId);
    }
}
