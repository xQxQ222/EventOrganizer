using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramUI.Keyboards
{
    public sealed class ReplyKeyboards
    {
        public ReplyKeyboardMarkup MainKeyBoard { get; private set; }

        public ReplyKeyboardMarkup AdminPanelKeyboard { get; private set; }

        public ReplyKeyboardMarkup RegistrateKeyboard { get; private set; }

        public ReplyKeyboardMarkup StartKeyboard { get; private set; }



        public ReplyKeyboards()
        {
            MainKeyBoard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Меню"),
                    new KeyboardButton("Профиль")
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

            RegistrateKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "Без почты", "С почтой" }
            })
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                };

            StartKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] {"Да", "Нет"}
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }
    }
}
