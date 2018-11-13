using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace ProjectTDD.view
{
    public class FarkleView
    {
        public virtual void DisplayDiceValues(int a_diceValue)
        {
            Console.WriteLine(a_diceValue);
        }

        public bool WantsToPlay()
        {
            throw new NotImplementedException();
        }
    }
}
