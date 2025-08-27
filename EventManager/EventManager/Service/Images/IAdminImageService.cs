using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.Images
{
    public interface IAdminImageService
    {
        Task<List<Image>> PostImages(long eventId, List<string> imagesPath);

        Task DeleteImages(List<long> imagesId);
    }
}
