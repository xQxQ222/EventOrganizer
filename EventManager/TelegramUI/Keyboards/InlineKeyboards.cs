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
        public InlineKeyboards()
        {
            StartKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Пройти регистрацию", "/registration")
                }
            });
        }
    }
}
