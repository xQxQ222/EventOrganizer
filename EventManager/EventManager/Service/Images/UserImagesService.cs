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

        public UserImagesService(EventManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Image>> GetEventImages(long eventId)
        {
            return dbContext.Images.Where(x=>x.EventId == eventId).ToList();
        }
    }
}
