using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool GetAction(string a_letter)
        {
            return a_letter != "q";
        }

        public virtual int GetAmountOfPlayers(bool a_isThisATest = false)
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Chose amount of players. Between [2] and [8] :");
                    Console.ResetColor();

                    input = GetNumberInput(a_isThisATest);

                    if (!input.All(c => c >= '2' && c <= '8'))
                    {
                        throw new ApplicationException();
                    }

                    return int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain a number between 2 and 8.\n");
                    Console.ResetColor();
                }
            }
        }

        private string GetNumberInput(bool a_isThisATest)
        {
            if(a_isThisATest)
            {
                return "3";
            } else
            {
                return Console.ReadLine();
            }
        }
    }
}
