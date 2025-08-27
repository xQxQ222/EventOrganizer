using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.CommentService
{
    public interface IAdminCommentService
    {
        Task DeleteComment(long userId, long commentId);

        Task<List<Comment>> GetUserComments(long userId, long commentatorId);

        Task<Comment> GetCommentById(long userId, long commentId);
    }
}
