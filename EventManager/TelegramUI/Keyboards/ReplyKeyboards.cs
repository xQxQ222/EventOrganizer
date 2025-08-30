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

        public ReplyKeyboardMarkup AdminMainKeyboard { get; private set; }

        public ReplyKeyboardMarkup ProfileKeyboard {  get; private set; }   


        public ReplyKeyboards()
        {
            MainKeyBoard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Меню"),
                    new KeyboardButton("Профиль")
                }
            })
            {
                ResizeKeyboard = true
            };

            AdminMainKeyboard = new ReplyKeyboardMarkup(new[]
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
                },
                new[] 
                {
                    new KeyboardButton("Панель администратора")
                }
            })
            {
                ResizeKeyboard = true,
            };

            ProfileKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Сменить email"),
                    new KeyboardButton("Вернуться в главное меню")
                }
            })
            {
                ResizeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetMainKeyboard(int userRoleId)
        {
            if(userRoleId == 1)
            {
                return AdminMainKeyboard;
            }
            return MainKeyBoard;
        }
    }
}
