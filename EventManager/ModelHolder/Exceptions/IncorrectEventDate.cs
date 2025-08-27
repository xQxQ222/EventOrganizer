using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Exceptions
{
    public class IncorrectEventDate : Exception
    {
        public IncorrectEventDate() : base($"Дата мероприятия должна быть не ранее следующего дня.") { }
    }
}
