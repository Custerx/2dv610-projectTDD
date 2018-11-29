using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model.console.readline
{
    public class ConsoleReadline : IConsoleReadline
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
