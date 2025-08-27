using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramUI.Answers
{
    public class MessageAnswers
    {
        public readonly ReplyKeyboardMarkup MainKeyBoard;
        public static Dictionary<string, Func<ITelegramBotClient, Chat, Task>> BotAnswers { get; set; }
        public MessageAnswers()
        {
            MainKeyBoard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Меню"),
                },
                new[]
                {
                    new KeyboardButton("Контакты"),
                    new KeyboardButton("Описание")
                }
            })
            {
                ResizeKeyboard = true
            };
        }

        public async Task AnswerToMessage(ITelegramBotClient client, Message message)
        {
            var me = client.GetMe();
            var myName = me.Result.FirstName;

        }
    }
}
