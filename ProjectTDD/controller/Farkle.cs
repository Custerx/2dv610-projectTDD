using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.controller
{
    public class Farkle
    {
        private model.Hand m_hand;
        private view.FarkleView m_farkleView;

        public Farkle(model.Hand a_hand, view.FarkleView a_farkleView)
        {
            m_hand = a_hand;
            m_farkleView = a_farkleView;
        }

        public void Start()
        {
            m_hand.Play();

            m_farkleView.DisplayDiceValues(1);
        }
    }
}
