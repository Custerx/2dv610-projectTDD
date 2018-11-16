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

        public Farkle(model.Hand a_hand, view.FarkleView a_farkleView)
        {
            m_hand = a_hand;
            m_farkleView = a_farkleView;
        }

        public void Start()
        {
            m_hand.Play();

            m_diceList = m_hand.Show();

            foreach(model.Dice dice in m_diceList)
            {
                m_farkleView.DisplayDice(dice);
            }

            if (m_farkleView.WantsToRollDice())
            {
                throw new NotImplementedException();
            }
        }
    }
}
