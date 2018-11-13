using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.exception
{
    public class EmptyListException : Exception
    {
        public EmptyListException()
        {
        }

        public EmptyListException(string message) : base(message)
        {
        }

        public EmptyListException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
