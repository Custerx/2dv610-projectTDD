using Castle.Core.Internal;
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
            Roll = 1,
            Save,
            NewGame,
            Quit
        }

        public void DisplayCannotSaveDiceTwice()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nError! You cannot save the same dice twice.\n");
            Console.ResetColor();
        }

        public void DisplaySaveKeys()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nSave Dices. Dice_1 = [1], Dice_2 = [2], Dice_3 = [3], Dice_4 = [4], Dice_5 = [5], Dice_6 = [6], EXIT = [7]\n");
            Console.ResetColor();
        }

        public model.Hand.Dices GetDiceToSave(bool a_isThisATest = false)
        {
            while (true)
            {
                try
                {
                    string input = GetNumberInput(a_isThisATest);

                    if (!input.All(c => c >= '1' && c <= '7'))
                    {
                        throw new ApplicationException();
                    }

                    return (model.Hand.Dices)int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain a number between 1 and 7.\n");
                    Console.ResetColor();
                }
            }
        }

        public string GetPlayername(string a_input = null)
        {
            while (true)
            {
                try
                {
                    GetPlayerNameIntroMessage();
                    return GetPlayerNameTestable(a_input);
                }
                catch (Exception)
                {
                    GetPlayerNameErrorMessage();
                }
            }

        }

        internal void GetPlayerNameIntroMessage()
        {

        }

        internal string GetPlayerNameTestable(string a_input)
        {
            string input = HandleInput(a_input);

            if (input.IsNullOrEmpty())
            {
                throw new ApplicationException();
            }

            return input;
        }

        internal void GetPlayerNameErrorMessage()
        {

        }

        public Action PlayerAction(string a_input = null)
        {
            while (true)
            {
                try
                {
                    return PlayerActionTestable(a_input);
                }
                catch (Exception)
                {
                    PlayerActionErrorMessage();
                }
            }
        }

        internal Action PlayerActionTestable(string a_input)
        {
            string input = HandleInput(a_input);

            if (!input.All(c => c >= '1' && c <= '4'))
            {
                throw new ApplicationException();
            }

            return (Action)int.Parse(input);
        }

        internal void PlayerActionErrorMessage()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nError! Your choice must contain a number between 1 and 4.\n");
            Console.ResetColor();
        }

        public void DisplayWinner(String a_player, int a_totalScore)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n{0} WON!\n", a_player);
            Console.Write("With a total-score of: {0}\n", a_totalScore);
            Console.ResetColor();
        }

        public void DisplayGameKeys()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n Roll = [1], Save dice(s) = [2], New game = [3], Quit game = [4].\n");
            Console.ResetColor();
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
            Console.Write("Total-score: {0}\n", a_totalScore);
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

        private string HandleInput(string a_input)
        {
            if (a_input == null)
            { 
                return Console.ReadLine();
            } else
            {
                return a_input;
            }
        }
    }
}
