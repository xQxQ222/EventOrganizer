using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.Images
{
    public interface IAdminImageService
    {
        Task<List<Image>> PostImages(long userId, long eventId, List<string> imagesPath);

        Task DeleteImages(long userId, List<long> imagesId);
    }
}
