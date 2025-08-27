using AutoMapper;
using ModelHolder.Exceptions;
using EventManager.Utility;
using ModelHolder.Context;
using ModelHolder.Dto;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.CommentService
{
    public class UserCommentService : IUserCommentService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly HelperMethods helperMethods;
        private readonly IMapper mapper;

        public UserCommentService(EventManagerDbContext dbContext, HelperMethods helperMethods, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.helperMethods = helperMethods;
            this.mapper = mapper;
        }

        public async Task<List<Comment>> GetEventComments(long userId, long eventId)
        {
            helperMethods.VerifyUserExistence(userId);

            return dbContext.Comments.Where(x=>x.EventId == eventId).ToList();
        }

        public async Task<List<Comment>> GetMyComments(long userId)
        {
            helperMethods.VerifyUserExistence(userId);
            return dbContext.Comments.Where(x=>x.AuthorId == userId).ToList();
        }

        public async Task<Comment> PostNewComment(long userId, CommentDto commentDto)
        {
            helperMethods.VerifyUserExistence(userId);
            var eventFromDb = dbContext.Events.Find(commentDto.EventId);
            if (eventFromDb == null)
            {
                throw new NotFoundException($"Мероприятие с id {commentDto.EventId} не найдено");
            }
            var comment = mapper.Map<Comment>(commentDto);
            comment.AuthorId = userId;

            await dbContext.Comments.AddAsync(comment);

            await dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> UpdateMyComment(long userId, long commentId, CommentUpdateDto commentDto)
        {
            helperMethods.VerifyUserExistence(userId);

            var comment = dbContext.Comments.Find(commentId);
            if (comment == null)
            {
                throw new NotFoundException($"Комментарий с id {commentId} не найден");
            }
            if (comment.AuthorId != userId)
            {
                throw new NotAnAuthorException($"Пользователь с id {userId} не является автором комментария с id {commentId}");
            }
            if (commentDto.Title != null)
            {
                comment.Title = commentDto.Title;
            }
            if (commentDto.Description != null)
            {
                comment.Description = commentDto.Description;
            }
            comment.IsPositive = commentDto.IsPositive;

            await dbContext.SaveChangesAsync();
            return comment;
        }
    }
}
