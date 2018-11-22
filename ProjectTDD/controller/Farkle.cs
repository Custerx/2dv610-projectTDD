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
            int players = m_IView.GetAmountOfPlayers();
            List<IPlayer> playerList = CreatePlayer(players);
            Play(playerList, a_noTest);
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

        internal void Play(List<IPlayer> a_playerList, bool a_noTest = true)
        {
            do
            {
                foreach (IPlayer player in a_playerList)
                {
                    m_IView.DisplayGameKeys();
                    m_IView.DisplayRolledDices("Rogge", player.GetHand(), player.CalculateScore(), player.GetTotalScore());
                    var action = m_IView.PlayerAction();
                }
            } while (a_noTest);
        }
    }
}
