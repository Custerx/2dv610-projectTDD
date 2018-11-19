﻿using System;
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

        public virtual int GetAmountOfPlayers()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Chose amount of players");
                    Console.WriteLine("\n [1], [2], [3], [4], [5], [6], [7] or [8]. Chose by typing a number between 1 and 8.\n");
                    Console.ResetColor();

                    input = "3"; // Console.ReadLine();

                    if (!input.All(c => c >= '1' && c <= '8'))
                    {
                        throw new ApplicationException();
                    }

                    return int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain a number between 1 and 8.\n");
                    Console.ResetColor();
                }
            }
        }
    }
}
