using AutoMapper;
using EventManager.Utility;
using ModelHolder.Context;
using ModelHolder.Dto;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.Images
{
    public class AdminImagesService : IAdminImageService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly HelperMethods helperMethods;

        public AdminImagesService(EventManagerDbContext dbContext, IMapper mapper, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.helperMethods = helperMethods;
        }

        public async Task DeleteImages(long userId, List<long> imagesId)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);
            foreach (long imageId in imagesId)
            {
                dbContext.Remove(imageId);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Image>> PostImages(long userId, long eventId, List<string> imagesPath)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);
            var eventFromDb = dbContext.Events.Find(eventId);
            if (eventFromDb == null)
            {
                throw new NotFoundException($"Мероприятие с id {eventId} не найдено");
            }
            foreach (var image in imagesPath)
            {
                ImageDto dto = new ImageDto(eventId, image);
                dbContext.Images.Add(mapper.Map<Image>(dto));
            }
            await dbContext.SaveChangesAsync();
            return dbContext.Images.Where(i => i.EventId == eventId).ToList();
        }
    }
}
