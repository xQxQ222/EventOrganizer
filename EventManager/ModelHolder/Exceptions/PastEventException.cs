using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Exceptions
{
    public class PastEventException : Exception
    {
        public PastEventException() : base("Дату мероприятия можно менять не позднее чем за 2 часа до начала мероприятия") { }
    }
}
