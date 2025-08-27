using System;

namespace ModelHolder.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Отказано в доступе") { } 
    }
}
