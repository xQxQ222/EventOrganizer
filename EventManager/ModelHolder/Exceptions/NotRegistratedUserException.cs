using System;

namespace ModelHolder.Exceptions
{
    public class NotRegistratedUserException : Exception
    {
        public NotRegistratedUserException(long userId) : base($"Пользователь с id {userId} не зарегистрирован") { }
    }
}
