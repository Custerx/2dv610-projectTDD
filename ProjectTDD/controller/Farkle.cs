using ProjectTDD.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDD.controller
{
    public class Farkle
    {
        private view.IView m_IView;
        private model.PlayerFactory m_playerFactory;
        private model.task.delay.IAsyncDelay m_asyncDelay;
        private readonly model.env.exit.IEnvironmentExit m_envExit;

        public Farkle(view.IView a_IView, model.env.exit.IEnvironmentExit a_envExit)
        {
            m_IView = a_IView;
            m_playerFactory = new PlayerFactory();
            m_asyncDelay = new model.task.delay.AsyncDelay();
            m_envExit = a_envExit;
        }

        public async Task Start(bool a_noTest = true)
        {
            try
            {
                m_IView.GetAmountOfPlayersIntroMessage();
                int players = m_IView.GetAmountOfPlayers();
                List<IPlayer> playerList = CreatePlayer(players);
                playerList = AddName(playerList);
                await Play(playerList, m_asyncDelay, a_noTest);
            }
            catch (Exception ex)
            {
                throw;
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
                m_IView.GetPlayerNameIntroMessage();
                string name = m_IView.GetPlayername();
                player.SetPlayername(name);
            }

            return a_playerList;
        }

        internal async Task Play(List<IPlayer> a_playerList, model.task.delay.IAsyncDelay a_asyncDelay, bool a_noTest = true)
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
                        await Roll(player, a_noTest, a_asyncDelay);
                        continue;
                    }

                    if (action == view.FarkleView.Action.Save)
                    {
                        await Save(player, a_noTest, a_asyncDelay);
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

        private async Task Roll(IPlayer player, bool a_noTest, model.task.delay.IAsyncDelay a_asyncDelay)
        {
            player.Roll();
            player.UpdateTotalScore();

            m_IView.DisplayRolledDices(player.GetPlayername(), player.GetHand(), player.CalculateScore(), player.GetTotalScore());

            await a_asyncDelay.Delay(TimeSpan.FromSeconds(2));

            if (player.IsPlayerWinner())
            {
                m_IView.DisplayWinner(player.GetPlayername(), player.GetTotalScore());

                await a_asyncDelay.Delay(TimeSpan.FromSeconds(5));

                if (a_noTest)
                {
                    await Start();
                }
            }
        }

        private async Task Save(IPlayer player, bool a_noTest, model.task.delay.IAsyncDelay a_asyncDelay)
        {
            List<model.Dice> diceList = player.GetHand();
            List<model.Dice> tempDiceList = new List<model.Dice>();
            List<model.Dice> dicesToBeSavedList = new List<model.Dice>();
            int dicesInList = 6;

            for (int i = 0; i < dicesInList; i++)
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
                    m_IView.DisplayCannotSaveDiceTwice();
                    if (a_noTest)
                    {
                        i--; // Enables endless loop and player can only exit through option "DONE".
                    }
                }
                else
                {
                    model.Dice diceToSave = diceList[index];
                    tempDiceList.Add(diceToSave);
                    diceList.RemoveAt(index);
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

            await RollRemainingDicesAfterSave(player, a_asyncDelay);
        }

        private async Task RollRemainingDicesAfterSave(IPlayer player, model.task.delay.IAsyncDelay a_asyncDelay)
        {
            if(player.IsMoreDicesToRoll())
            {
                player.Roll();
            }
   
            player.UpdateTotalScore();

            m_IView.DisplayRolledDices(player.GetPlayername(), player.GetHand(), player.CalculateScore(), player.GetTotalScore());

            await a_asyncDelay.Delay(TimeSpan.FromSeconds(2));

            player.Roll(); // Roll dices again, orelse same dice value will appear next round.
        }

        private void Quit(bool a_noTest)
        {
            m_envExit.Exit(0);
        }
    }
}
