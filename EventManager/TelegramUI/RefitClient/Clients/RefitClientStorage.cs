using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramUI.RefitClient.CategoryClient;
using TelegramUI.RefitClient.Comments;
using TelegramUI.RefitClient.Events;
using TelegramUI.RefitClient.Images;
using TelegramUI.RefitClient.Requests;
using TelegramUI.RefitClient.Users;

namespace TelegramUI.RefitClient.Clients
{
    public sealed class RefitClientStorage
    {
        private const string API_URI = "https://localhost:7184";

        public readonly IUserApi userApi;
        public readonly ICategoryApi categoryApi;
        public readonly ICommentsApi commentApi;
        public readonly IEventApi eventApi;
        public readonly IImagesApi imagesApi;
        public readonly IRequestApi requestsApi;

        public RefitClientStorage()
        {
            userApi = RestService.For<IUserApi>(API_URI);
            categoryApi = RestService.For<ICategoryApi>(API_URI);
            commentApi = RestService.For<ICommentsApi>(API_URI);
            eventApi = RestService.For<IEventApi>(API_URI);
            imagesApi = RestService.For<IImagesApi>(API_URI);
            requestsApi = RestService.For<IRequestApi>(API_URI);
        }
    }
}
