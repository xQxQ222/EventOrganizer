using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Exceptions
{
    public class IncorrectRequestStatusException : Exception
    {
        public IncorrectRequestStatusException() : base("Изменять параметры заявки можно только при статусе заявки PENDING") { }
    }
}
