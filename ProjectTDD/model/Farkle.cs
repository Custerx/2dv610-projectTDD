using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    class Farkle
    {
        private Dice m_dice_1;
        private Dice m_dice_2;
        private Dice m_dice_3;
        private Dice m_dice_4;
        private Dice m_dice_5;
        private Dice m_dice_6;

        public enum Dices
        {
            Dice_1,
            Dice_2,
            Dice_3,
            Dice_4,
            Dice_5,
            Dice_6
        }

        public Farkle()
        {
            m_dice_1 = new Dice();
            m_dice_2 = new Dice();
            m_dice_3 = new Dice();
            m_dice_4 = new Dice();
            m_dice_5 = new Dice();
            m_dice_6 = new Dice();
        }

        public List<model.Dice> Play()
        {
            List<model.Dice> diceList = new List<model.Dice>();

            return diceList;
        }
    }
}
