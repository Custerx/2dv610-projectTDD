using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.exception
{
    public class EmptyDiceListException : Exception
    {
        public EmptyDiceListException()
        {
        }

        public EmptyDiceListException(string message) : base(message)
        {
        }

        public EmptyDiceListException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
