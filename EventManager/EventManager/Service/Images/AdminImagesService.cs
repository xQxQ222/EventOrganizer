using ModelHolder.Context;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.Images
{
    public class AdminImagesService : IAdminImageService
    {

        private readonly EventManagerDbContext dbContext;

        public AdminImagesService(EventManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteImages(List<long> imagesId)
        {
            foreach (long imageId in imagesId)
            {
                dbContext.Remove(imageId);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Image>> PostImages(long eventId, List<string> imagesPath)
        {
            //var eventFromDb = dbContext.Find
        }
    }
}
