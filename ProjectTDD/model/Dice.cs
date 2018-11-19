using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    public class Dice
    {
        private static Random m_random = new Random();
        private DiceValue m_value;

        public Dice()
        {
            m_value = 0;
        }

        public enum DiceValue
        {
            One = 1,
            Two,
            Three,
            Four,
            Five,
            Six
        }

        public void Roll()
        {
            m_value = (DiceValue)m_random.Next(1, 7);
        }

        public virtual DiceValue GetValue()
        {
            return m_value;
        }

        public virtual model.Hand.Dices Dicenumber { get; set; }
    }
}
