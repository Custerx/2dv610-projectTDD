using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.exception
{
    class DiceNotFoundException : Exception
    {
        public DiceNotFoundException() : base()
        {
        }

        public DiceNotFoundException(string message) : base(message)
        {
        }

        public DiceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
