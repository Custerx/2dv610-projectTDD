using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.exception
{
    public class InvalidStringArgumentException : Exception
    {
        public InvalidStringArgumentException() : base()
        {
        }

        public InvalidStringArgumentException(string message) : base(message)
        {
        }

        public InvalidStringArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
