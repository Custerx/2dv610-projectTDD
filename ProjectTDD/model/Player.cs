using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    public class Player
    {
        private model.Hand m_hand;

        public Player(model.Hand a_hand)
        {
            m_hand = a_hand;
        }

        public virtual List<model.Dice> GetHand()
        {
            return m_hand.Show();
        }

        public virtual void Play()
        {
            throw new NotImplementedException();
        }
    }
}
