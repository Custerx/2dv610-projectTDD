using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.controller
{
    public class Farkle
    {
        private model.Hand m_hand;
        private view.FarkleView m_farkleView;

        private string placeholderPlayer = "Rogge";
        private int placeholderScore = 300;

        public Farkle(model.Hand a_hand, view.FarkleView a_farkleView)
        {
            m_hand = a_hand;
            m_farkleView = a_farkleView;
        }

        public void Start()
        {
            int players = m_farkleView.GetAmountOfPlayers();

            m_hand.Play();

            m_farkleView.DisplayRolledDices(placeholderPlayer, m_hand.Show(), placeholderScore);

            if (m_farkleView.WantsToRollDice())
            {
                throw new NotImplementedException();
            }
        }

        internal virtual List<model.Player> CreatePlayer(int players)
        {
            List<model.Player> playerList = new List<model.Player>();

            for (int i = 0; i < players; i++)
            {
                playerList.Add(new model.Player(new model.Hand()));
            }

            return playerList;
        }
    }
}
