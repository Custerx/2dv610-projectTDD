using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    public class Dice
    {
        private static Random m_random = new Random();
        private int m_value;

        public Dice()
        {
            m_value = 0;
        }

        public void Roll()
        {
            m_value = m_random.Next(1, 7);
        }

        public int GetValue()
        {
            return m_value;
        }

        public model.Hand.Dices Dicenumber { get; set; }
    }
}
