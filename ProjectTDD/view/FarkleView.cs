using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace ProjectTDD.view
{
    public class FarkleView
    {
        public virtual int GetAmountOfPlayers()
        {
            throw new NotImplementedException();
        }

        public virtual void DisplayDice(model.Dice a_dice)
        {
            Console.WriteLine("{0} : {1}", a_dice.Dicenumber, a_dice.GetValue());
        }

        public virtual void DisplayRolledDices(String a_player, List<model.Dice> a_hand, int a_score)
        {
            Console.WriteLine("{0} Rolled: ", a_player);
            foreach (model.Dice d in a_hand)
            {
                DisplayDice(d);
            }
            Console.WriteLine("Score: {0}", a_score);
            Console.WriteLine("");
        }

        public virtual bool WantsToRollDice()
        {
            // TODO Make readkey testable.

            return false;
        }
    }
}
