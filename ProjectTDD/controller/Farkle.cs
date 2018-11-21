using ProjectTDD.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.controller
{
    public class Farkle
    {
        private view.FarkleView m_farkleView;
        private model.PlayerFactory m_playerFactory;

        public Farkle(view.FarkleView a_farkleView)
        {
            m_farkleView = a_farkleView;
            m_playerFactory = new PlayerFactory();
        }

        public bool Action(IPlayer player, string a_letter = null, bool a_test = false)
        {
            return m_farkleView.GetAction(player, a_letter, a_test);
        }

        public void Start()
        {
            int players = m_farkleView.GetAmountOfPlayers();
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
                m_farkleView.DisplayRolledDices("Rogge", player.GetHand(), player.CalculateScore());
            }
        }
    }
}
