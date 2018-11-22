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

        public void Start()
        {
            int players = m_IView.GetAmountOfPlayers();
            List<IPlayer> playerList = CreatePlayer(players);
            Play(playerList);
        }

        internal virtual List<IPlayer> CreatePlayer(int a_players)
        {
            List<IPlayer> playerList = new List<IPlayer>();

            for (int i = 0; i < a_players; i++)
            {
                playerList.Add(m_playerFactory.CreateNewPlayer());
            }

            return playerList;
        }

        internal virtual void Play(List<IPlayer> a_playerList)
        {
            foreach(IPlayer player in a_playerList)
            {
                player.Play();
                m_IView.DisplayRolledDices("Rogge", player.GetHand(), player.CalculateScore());
            }
        }
    }
}
