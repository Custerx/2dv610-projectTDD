using ProjectTDD.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.controller
{
    public class Farkle
    {
        private view.FarkleView m_farkleView;

        public Farkle(view.FarkleView a_farkleView)
        {
            m_farkleView = a_farkleView;
        }

        public void Start()
        {
            int players = m_farkleView.GetAmountOfPlayers();
            List<model.Player> playerList = CreatePlayer(players);
        }

        internal virtual List<model.Player> CreatePlayer(int a_players)
        {
            List<model.Player> playerList = new List<model.Player>();

            for (int i = 0; i < a_players; i++)
            {
                playerList.Add(new model.Player(new model.Hand()));
            }

            return playerList;
        }

        internal virtual void Play(List<model.Player> a_playerList)
        {
            throw new NotImplementedException();
        }
    }
}
