using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramUI.Answers;
using TelegramUI.Configuration;
using TelegramUI.RefitClient.CategoryClient;
using TelegramUI.RefitClient.Comments;
using TelegramUI.RefitClient.Events;
using TelegramUI.RefitClient.Images;
using TelegramUI.RefitClient.Requests;
using TelegramUI.RefitClient.Users;

namespace TelegramUI;

class Program
{
    static IHttpClientFactory _httpClientFactory;
    private static MessageAnswers messageAnswers;
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .BuildServiceProvider();

        _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        messageAnswers = new MessageAnswers();

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        Config.SetProperties(config);
        
        StartBot(config);
    }

    public static async void StartBot(IConfiguration configuration)
    {
        var _botClient = new TelegramBotClient(Config.BotSettings.TELEGRAM_TOKEN);
        var _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { },
            DropPendingUpdates = true,
        };
        using var cts = new CancellationTokenSource();
        _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);
        var me =  _botClient.GetMe();
        Console.WriteLine($"{me.Result.FirstName} started");
        Console.ReadLine();
    }

    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                await messageAnswers.AnswerToMessage(botClient, update.Message);
                break;
            case UpdateType.CallbackQuery:
                await messageAnswers.AnswerToCallbackQuery(botClient, update.CallbackQuery);
                break;
        }
    }

    private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        var ErrorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };
        Console.WriteLine("Было вызвано исключение:");
        Console.WriteLine(ErrorMessage);
        Console.WriteLine();
        return Task.CompletedTask;
    }
}