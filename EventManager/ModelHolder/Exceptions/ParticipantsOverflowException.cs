using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Exceptions
{
    public class ParticipantsOverflowException : Exception
    {
        public ParticipantsOverflowException(long eventId) : base($"Количество участников мероприятия с id {eventId} превышено") { }
    }
}
