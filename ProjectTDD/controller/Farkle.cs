using ProjectTDD.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.controller
{
    public class Farkle
    {
        private view.IView m_IView;
        private model.PlayerFactory m_playerFactory;

        public Farkle(view.IView a_IView)
        {
            m_IView = a_IView;
            m_playerFactory = new PlayerFactory();
        }

        public bool Action(IPlayer player, string a_letter = null, bool a_test = false)
        {
            return m_IView.GetAction(player, a_letter, a_test);
        }

        public void Start(bool a_noTest = true)
        {
            try
            {
                int players = m_IView.GetAmountOfPlayers();
                List<IPlayer> playerList = CreatePlayer(players);
                playerList = AddName(playerList);
                Play(playerList, a_noTest);
            }
            catch (model.exception.DiceNotFoundException dnfex)
            {
                Console.WriteLine(dnfex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        internal List<IPlayer> CreatePlayer(int a_players)
        {
            List<IPlayer> playerList = new List<IPlayer>();

            for (int i = 0; i < a_players; i++)
            {
                playerList.Add(m_playerFactory.CreateNewPlayer());
            }

            return playerList;
        }

        internal List<IPlayer> AddName(List<IPlayer> a_playerList)
        {
            foreach (IPlayer player in a_playerList)
            {
                string name = m_IView.GetPlayername();
                player.SetPlayername(name);
            }

            return a_playerList;
        }

        internal void Play(List<IPlayer> a_playerList, bool a_noTest = true)
        {
            do
            {
                foreach (IPlayer player in a_playerList)
                {
                    m_IView.DisplayGameKeys();
                    m_IView.DisplayRolledDices(player.GetPlayername(), player.GetHand(), player.CalculateScore(), player.GetTotalScore());
                    var action = m_IView.PlayerAction();

                    if (action == view.FarkleView.Action.NewGame)
                    {
                        NewGame(a_noTest);
                    }

                    if (action == view.FarkleView.Action.Roll)
                    {
                        Roll(player, a_noTest);
                        continue;
                    }

                    if (action == view.FarkleView.Action.Save)
                    {
                        Save(player, a_noTest);
                    }

                    if (action == view.FarkleView.Action.Quit)
                    {
                        Quit(a_noTest);
                    }
                }
            } while (a_noTest);
        }

        private void NewGame(bool a_noTest)
        {
            if (a_noTest)
            {
                Start();
            }

            throw new model.exception.ValidateNewGameException();
        }

        private void Roll(IPlayer player, bool a_noTest)
        {
            player.Roll();
            player.UpdateTotalScore();

            m_IView.DisplayRolledDices(player.GetPlayername(), player.GetHand(), player.CalculateScore(), player.GetTotalScore());

            if (a_noTest) // To avoid 2 sec delay when testing.
            {
                System.Threading.Thread.Sleep(2000);
            }

            if (player.IsPlayerWinner())
            {
                m_IView.DisplayWinner(player.GetPlayername(), player.GetTotalScore());

                if (a_noTest) // To avoid 5 sec delay when testing.
                {
                    System.Threading.Thread.Sleep(5000);
                    Start();
                }
            }
        }

        private void Save(IPlayer player, bool a_noTest)
        {
            List<model.Dice> diceList = player.GetHand();
            List<model.Dice> tempDiceList = new List<model.Dice>();
            List<model.Dice> dicesToBeSavedList = new List<model.Dice>();

            for (int i = 0; i < diceList.Count; i++)
            {
                m_IView.DisplaySaveKeys();
                model.Hand.Dices input = m_IView.GetDiceToSave();

                if (input == model.Hand.Dices.Done)
                {
                    break;
                }

                int index = diceList.FindIndex(d => d.Dicenumber == input);

                if (index == -1)
                {
                    throw new model.exception.DiceNotFoundException();
                }
                else
                {
                    model.Dice diceToSave = diceList[index];
                    tempDiceList.Add(diceToSave);
                }
            }

            foreach (model.Dice d in tempDiceList)
            {
                dicesToBeSavedList.Add(d);
            }

            for (int i = 0; i < dicesToBeSavedList.Count; i++)
            {
                player.Save(dicesToBeSavedList[i]);
            }

            RollRemainingDicesAfterSave(player, a_noTest);
        }

        private void RollRemainingDicesAfterSave(IPlayer player, bool a_noTest)
        {

        }

        private void Quit(bool a_noTest)
        {
            if (a_noTest)
            {
                Environment.Exit(0);
            }

            throw new model.exception.ValidateQuitException();
        }
    }
}
