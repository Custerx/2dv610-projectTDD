using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.exception
{
    class ValidateQuitException : Exception
    {
        public ValidateQuitException() : base()
        {
        }

        public ValidateQuitException(string message) : base(message)
        {
        }

        public ValidateQuitException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
