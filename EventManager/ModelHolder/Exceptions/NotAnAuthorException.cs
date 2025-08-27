using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Exceptions
{
    public class NotAnAuthorException : Exception
    {
        public NotAnAuthorException(string message) : base(message) { }
    }
}
