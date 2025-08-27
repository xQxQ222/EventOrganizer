using EventManager.Utility;
using ModelHolder.Context;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.Images
{
    public class UserImagesService : IUserImagesService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly HelperMethods helperMethods;

        public UserImagesService(EventManagerDbContext dbContext, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.helperMethods = helperMethods;
        }

        public async Task<List<Image>> GetEventImages(long userId, long eventId)
        {
            helperMethods.VerifyUserExistence(userId);
            return dbContext.Images.Where(x=>x.EventId == eventId).ToList();
        }
    }
}
