using EventManager.Service.Parsers;
using ModelHolder.Dto;
using System.Collections.Concurrent;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramUI.Enums;
using TelegramUI.Keyboards;
using TelegramUI.RefitClient.Clients;

namespace TelegramUI.Answers
{
    public class MessageAnswers
    {
        private readonly ReplyKeyboards replyKeyboards;
        private readonly InlineKeyboards inlineKeyboards;
        private readonly Dictionary<string, Func<ITelegramBotClient, Message, Task>> BotMessageAnswers;
        private readonly Dictionary<string, Func<ITelegramBotClient, CallbackQuery, Task>> BotQueryAnswers;
        
        private ConcurrentDictionary<long, UserAction> currentAction = new();
        private ConcurrentDictionary<long, UserState> userReg = new();
        private ConcurrentDictionary<long, ReplyKeyboardMarkup> userMainKeyboards = new();

        private RefitClientStorage refitClientStorage;

        private readonly ConcurrentDictionary<long, string> userEmails = new();
        public MessageAnswers()
        {
            replyKeyboards = new ReplyKeyboards();
            inlineKeyboards = new InlineKeyboards();

            refitClientStorage = new RefitClientStorage();

            BotMessageAnswers = new Dictionary<string, Func<ITelegramBotClient, Message, Task>>()
            {
                {"/start",AnswerToStart },
                {"Профиль", AnswerToProfile},
                {"/createEvent",  PrepareToCreateEvent}
            };

            BotQueryAnswers = new Dictionary<string, Func<ITelegramBotClient, CallbackQuery, Task>>()
            {
                {"/registration", AnswerToRegistrate }
                
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
            if (!userReg.ContainsKey(message.From.Id))
            {
                userReg[message.From.Id] = UserState.None;
            }
            Console.WriteLine($"Пользователь {message.Chat.Username} написал: {message.Text}.");
            try
            {
                if (userReg[message.From.Id] == UserState.None)
                {
                    await client.SendMessage(message.Chat.Id, $"Пройдите регистрацию, чтобы пользоваться ботом", replyMarkup: inlineKeyboards.StartKeyboard);
                    return;
                }
                else if (userReg[message.From.Id] == UserState.AwaitingEmail)
                {
                    UserDto userDto = new UserDto(message.Text);
                    var user = await refitClientStorage.userApi.RegisterNewUser(message.From.Id, userDto);
                    userMainKeyboards[user.TelegramId] = replyKeyboards.GetMainKeyboard(user.RoleId);
                    await client.SendMessage(message.Chat.Id, $"Успешная регистрация ✓", replyMarkup: userMainKeyboards[message.From.Id]);
                    userReg[message.From.Id] = UserState.Registered;
                    return;
                }
                else
                {
                    if (!currentAction.ContainsKey(message.From.Id))
                    {
                        currentAction[message.From.Id] = UserAction.None;
                    }
                    if (currentAction[message.From.Id] == UserAction.Adding_Event)
                    {
                        var eventDto = ParsersStorage.ParseEvent(message.Text);
                        await refitClientStorage.eventApi.AddNewEvent(message.From.Id, eventDto);
                    }
                    switch (BotMessageAnswers.ContainsKey(message.Text))
                    {
                        case true:
                            var func = BotMessageAnswers[message.Text];
                            await func(client, message);
                            break;
                        case false:
                            await client.SendMessage(message.Chat.Id, $"Я не знаю такую команду: {message.Text}", replyMarkup: userMainKeyboards[message.From.Id]);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                await client.SendMessage(message.Chat.Id, ex.Message);
            }
        }

        public async Task AnswerToStart(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendMessage(message.Chat.Id, $"Добро пожаловать в телеграмм-бот для управления и организации мероприятий!");
        }

        public async Task AnswerToRegistrate(ITelegramBotClient botClient, CallbackQuery query)
        {
            await botClient.SendMessage(query.Message.Chat.Id, $"Приветствую! Введите свой email для регистрации");
            userReg[query.From.Id] = UserState.AwaitingEmail;
        }

        public async Task AnswerToProfile(ITelegramBotClient botClient, Message message)
        {
            var user = await refitClientStorage.userApi.GetProfile(message.From.Id);
            await botClient.SendMessage(message.Chat.Id, $"Ваша почта: {user.Email}, Id роли: {user.RoleId}", replyMarkup: replyKeyboards.ProfileKeyboard);
        }

        public async Task PrepareToCreateEvent(ITelegramBotClient botClient, Message message)
        {
            var categories = await refitClientStorage.categoryApi.GetAllCategories(message.From.Id);
            string preparing = """
                Введите информацию о мероприятии в формате:
                Название: xxxxxx
                Описание: xxxxxx
                Локация: Широта Долгота
                Дата: dd.MM.YYYY hh:mm
                Платно: да
                Лимит: 100
                Категория: id категории
                
                """;
            await botClient.SendMessage(message.Chat.Id, preparing);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Список доступных категорий (id - name)");
            foreach (var category in categories)
            {
                stringBuilder.AppendLine($"{category.CategoryId} - {category.CategoryName}");
            }
            await botClient.SendMessage(message.Chat.Id, stringBuilder.ToString());
            currentAction[message.From.Id] = UserAction.Adding_Event;
        }
    }
}
