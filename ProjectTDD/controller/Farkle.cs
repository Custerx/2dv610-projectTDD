using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.controller
{
    public class Farkle
    {
        private model.Hand m_hand;
        private view.FarkleView m_farkleView;
        private List<model.Dice> m_diceList;

        private string placeholderPlayer = "Rogge";
        private int placeholderScore = 300;

        public Farkle(model.Hand a_hand, view.FarkleView a_farkleView)
        {
            m_hand = a_hand;
            m_farkleView = a_farkleView;
        }

        public void Start()
        {
            m_hand.Play();

            m_diceList = m_hand.Show();

            m_farkleView.DisplayRolledDices(placeholderPlayer, m_diceList, placeholderScore);

            if (m_farkleView.WantsToRollDice())
            {
                throw new NotImplementedException();
            }
        }
    }
}
