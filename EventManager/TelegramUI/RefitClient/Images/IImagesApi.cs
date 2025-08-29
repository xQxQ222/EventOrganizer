using ModelHolder.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramUI.RefitClient.Images
{
    public interface IImagesApi
    {
        [Post("/api/admin/images/event/{eventId}")]
        Task<List<Image>> PostImages([Header("X-User-Id")] long userId, long eventId, [Body] List<string> imagesPath);

        [Delete("/api/admin/images")]
        Task DeleteImages([Header("X-User-Id")] long userId, [Body] List<long> imagesIds);

        [Get("/api/images/event/{eventId}")]
        Task<List<Image>> GetEventImages([Header("X-User-Id")] long userId, long eventId);
    }
}
