using Refit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramUI.Keyboards;
using TelegramUI.RefitClient.CategoryClient;
using TelegramUI.RefitClient.Comments;
using TelegramUI.RefitClient.Events;
using TelegramUI.RefitClient.Images;
using TelegramUI.RefitClient.Requests;
using TelegramUI.RefitClient.Users;

namespace TelegramUI.Answers
{
    public class MessageAnswers
    {
        private readonly ReplyKeyboards replyKeyboards;
        private readonly InlineKeyboards inlineKeyboards;
        public Dictionary<string, Func<ITelegramBotClient, Message, Task>> BotMessageAnswers { get; set; }
        public Dictionary<string, Func<ITelegramBotClient, CallbackQuery, Task>> BotQueryAnswers { get; set; }

        private ConcurrentDictionary<long, List<Message>> userMessages = new();
        private const string apiUri = "https://localhost:5001";

        private readonly IUserApi userApi;
        private readonly ICategoryApi categoryApi;
        private readonly ICommentsApi commentApi;
        private readonly IEventApi eventApi;
        private readonly IImagesApi imagesApi;
        private readonly IRequestApi requestsApi;

        private readonly ConcurrentDictionary<long, string> userEmails = new();
        public MessageAnswers()
        {
            replyKeyboards = new ReplyKeyboards();
            inlineKeyboards = new InlineKeyboards();

            userApi = RestService.For<IUserApi>(apiUri);
            categoryApi = RestService.For<ICategoryApi>(apiUri);
            commentApi = RestService.For<ICommentsApi>(apiUri);
            eventApi = RestService.For<IEventApi>(apiUri);
            imagesApi = RestService.For<IImagesApi>(apiUri);
            requestsApi = RestService.For<IRequestApi>(apiUri);

            BotMessageAnswers = new Dictionary<string, Func<ITelegramBotClient, Message, Task>>()
            {
                {"/start",AnswerToStart }
            };

            BotQueryAnswers = new Dictionary<string, Func<ITelegramBotClient, CallbackQuery, Task>>()
            {
                {"/registration", AnswerToRegistrate },
                {"/register_with_email", AnswerToRegistrateWithEmail }
            };
        }

        public async Task AnswerToCallbackQuery(ITelegramBotClient client, CallbackQuery query)
        {
            var me = client.GetMe();
            var myName = me.Result.FirstName;

            Console.WriteLine($"Пользователь {query.From} написал: {query.Data}");
            switch (BotQueryAnswers.ContainsKey(query.Data))
            {
                case true:
                    var func = BotQueryAnswers[query.Data];
                    await func(client, query);
                    break;
                case false:
                    await client.SendMessage(query.Message.Chat.Id, $"Я не знаю такую команду: {query.Message.Text}", replyMarkup: replyKeyboards.MainKeyBoard);
                    break;
            }
        }

        public async Task AnswerToMessage(ITelegramBotClient client, Message message)
        {
            var me = client.GetMe();
            var myName = me.Result.FirstName;

            Console.WriteLine($"Пользователь {message.Chat.Username} написал: {message.Text}");
            switch (BotMessageAnswers.ContainsKey(message.Text))
            {
                case true:
                    var func = BotMessageAnswers[message.Text];
                    await func(client, message);
                    break;
                case false:
                    await client.SendMessage(message.Chat.Id, $"Я не знаю такую команду: {message.Text}", replyMarkup: replyKeyboards.MainKeyBoard);
                    break;
            }
        }

        public async Task AnswerToStart(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendMessage(message.Chat.Id, $"Добро пожаловать в телеграмм-бот для управления и организации мероприятий!");
            await botClient.SendMessage(message.Chat.Id, $"Вы уже зарегистрированы?", replyMarkup: inlineKeyboards.StartKeyboard);
        }

        public async Task AnswerToRegistrate(ITelegramBotClient botClient, CallbackQuery query)
        {
            await botClient.SendMessage(query.Message.Chat.Id, $"Приветствую! Как будем регистрироваться?", replyMarkup: replyKeyboards.RegistrateKeyboard);
        }

        public async Task AnswerToRegistrateWithEmail(ITelegramBotClient botClient, CallbackQuery query)
        {
            await botClient.SendMessage(query.Message.Chat.Id, $"Отлично! Введите свой email");
        }
    }
}
