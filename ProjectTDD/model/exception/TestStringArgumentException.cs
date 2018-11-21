using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.exception
{
    public class TestStringArgumentException : Exception
    {
        public TestStringArgumentException() : base()
        {
        }

        public TestStringArgumentException(string message) : base(message)
        {
        }

        public TestStringArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
