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

        public virtual List<model.Dice> GetSavedHand()
        {
            return m_hand.ShowSaved();
        }

        public virtual void Play()
        {
            m_hand.Play();
        }

        public virtual void Roll()
        {
            m_hand.Roll();
        }

        public virtual void Save(model.Dice a_dice)
        {
            throw new NotImplementedException();
        }
    }
}
