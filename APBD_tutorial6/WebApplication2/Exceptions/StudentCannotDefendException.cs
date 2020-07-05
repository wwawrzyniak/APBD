using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Exceptions
{
    public class StudentCannotDefendException : Exception
    {
        public StudentCannotDefendException(string message) : base(message)
        {
        }

        public StudentCannotDefendException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
