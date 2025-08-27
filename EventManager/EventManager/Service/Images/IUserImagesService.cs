using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.Images
{
    public interface IUserImagesService
    {
        Task<List<Image>> GetEventImages(long eventId);
    }
}
