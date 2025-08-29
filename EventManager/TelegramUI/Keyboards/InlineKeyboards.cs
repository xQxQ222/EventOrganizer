using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramUI.Keyboards
{
    public sealed class InlineKeyboards
    {
        public InlineKeyboardMarkup StartKeyboard { get; private set; }
        public InlineKeyboardMarkup RegisterKeyboard {  get; private set; }
        public InlineKeyboards()
        {
            StartKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Да", "/already-register"),
                    InlineKeyboardButton.WithCallbackData("Нет", "/registration")
                }
            });

            RegisterKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("С почтой","/register_with_email"),
                    InlineKeyboardButton.WithCallbackData("Без почты", "/register_without_email")
                }
            });
        }
    }
}
