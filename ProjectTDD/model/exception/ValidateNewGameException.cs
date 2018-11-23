using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.exception
{
    class ValidateNewGameException : Exception
    {
        public ValidateNewGameException() : base()
        {
        }

        public ValidateNewGameException(string message) : base(message)
        {
        }

        public ValidateNewGameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
