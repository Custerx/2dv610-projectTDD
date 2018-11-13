using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.view
{
    public class FarkleView
    {
        public virtual void DisplayDiceValues(int a_diceValue)
        {
            Console.WriteLine(a_diceValue);
        }
    }
}
