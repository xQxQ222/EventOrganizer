using EventManager.Utility;
using ModelHolder.Context;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.CommentService
{
    public class AdminCommentService : IAdminCommentService
    {
        private readonly EventManagerDbContext dbContext;
        private readonly HelperMethods helperMethods;

        public AdminCommentService(EventManagerDbContext dbContext, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.helperMethods = helperMethods;
        }

        public async Task DeleteComment(long userId, long commentId)
        {
            helperMethods.VerifyUserExistence(userId);
            var comment = dbContext.Comments.Find(commentId);
            if (comment == null)
            {
                throw new NotFoundException($"Комментарий с id {commentId} не найден");
            }
            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Comment> GetCommentById(long userId, long commentId)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);

            var comment = dbContext.Comments.Find(commentId);
            if (comment == null)
            {
                throw new NotFoundException($"Комментарий с id {commentId} не найден");
            }

            return comment;
        }

        public async Task<List<Comment>> GetUserComments(long userId, long commentatorId)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);

            return dbContext.Comments.Where(x=>x.AuthorId == commentatorId).ToList();
        }
    }
}
