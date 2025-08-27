using ModelHolder.Dto;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.CommentService
{
    public interface IUserCommentService
    {
        Task<Comment> PostNewComment(long userId, CommentDto commentDto);

        Task<List<Comment>> GetMyComments(long userId);

        Task<Comment> UpdateMyComment(long userId, long commentId, CommentUpdateDto commentDto);

        Task<List<Comment>> GetEventComments(long userId, long eventId);
    }
}
