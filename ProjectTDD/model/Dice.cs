using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    class Dice
    {
        private static Random m_random = new Random();

        public int GetValue()
        {
            return m_random.Next(1, 7);
        }
    }
}
