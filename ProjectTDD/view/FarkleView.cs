using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace ProjectTDD.view
{
    public class FarkleView
    {
        public virtual void DisplayDice(model.Dice a_dice)
        {
            Console.WriteLine("{0} : {1}", a_dice.Dicenumber, a_dice.GetValue());
        }

        public virtual bool WantsToRollDice()
        {
            // TODO Make readkey testable.

            return false;
        }
    }
}
