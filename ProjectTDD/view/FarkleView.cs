using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Sdk;

namespace ProjectTDD.view
{
    public class FarkleView : IView
    {
        public enum Action
        {
            NewGame = 1,
            Roll,
            Save,
            Quit
        }

        public Action PlayerAction(bool a_isThisATest = false)
        {
            while (true)
            {
                try
                {
                    string input = GetNumberInput(a_isThisATest);

                    if (!input.All(c => c >= '1' && c <= '4'))
                    {
                        throw new ApplicationException();
                    }

                    return (Action)int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain a number between 1 and 4.\n");
                    Console.ResetColor();
                }
            }
        }

        public void DisplayGameKeys()
        {
            Console.Write("Start new game: [1]. Roll non-saved dice(s): [2]. Save dice(s): [3]. Quit game: [4].\n");
        }

        public void DisplayDice(model.Dice a_dice)
        {
            Console.Write("{0} : {1}\n", a_dice.Dicenumber, a_dice.GetValue());
        }

        public void DisplayRolledDices(String a_player, List<model.Dice> a_hand, int a_score, int a_totalScore)
        {
            Console.Write("{0} Rolled: \n", a_player);
            foreach (model.Dice d in a_hand)
            {
                DisplayDice(d);
            }
            Console.Write("Score: {0}\n", a_score);
        }

        public bool GetAction(model.IPlayer player, string a_letter = null, bool a_test = false)
        {
            if (a_test && !a_letter.All(c => c == 'r' || c == 'n' || c == 's' || c == 'q'))
            {
                throw new model.exception.TestStringArgumentException();
            }

            if (a_test == false && a_letter != null)
            {
                throw new model.exception.InvalidStringArgumentException();
            }

            if (a_test == false)
            {
                a_letter = Console.ReadLine();
            }

            if (a_letter == "r")
            {
                player.Roll();
            }

            if (a_letter == "s") // TODO: Enable user to chose what dice to save.
            {
                List<model.Dice> diceList = player.GetHand();
                foreach (model.Dice d in diceList)
                {
                    player.Save(d);
                }
            }

            return a_letter != "q";
        }

        public int GetAmountOfPlayers(bool a_isThisATest = false)
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
