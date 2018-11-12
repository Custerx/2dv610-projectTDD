﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectTDD.model
{
    class Farkle
    {
        List<model.Dice> m_diceList;
        List<model.Dice> m_savedDiceList;
        private Dice m_dice_1;
        private Dice m_dice_2;
        private Dice m_dice_3;
        private Dice m_dice_4;
        private Dice m_dice_5;
        private Dice m_dice_6;

        public enum Dices
        {
            Dice_1 = 1,
            Dice_2,
            Dice_3,
            Dice_4,
            Dice_5,
            Dice_6
        }

        public Farkle()
        {
            m_diceList = new List<model.Dice>();
            m_savedDiceList = new List<model.Dice>();
            m_dice_1 = new Dice();
            m_dice_2 = new Dice();
            m_dice_3 = new Dice();
            m_dice_4 = new Dice();
            m_dice_5 = new Dice();
            m_dice_6 = new Dice();
        }

        public List<model.Dice> Play()
        {
            AddDiceNrAndRollThenAddToList(m_dice_1, 1);
            AddDiceNrAndRollThenAddToList(m_dice_2, 2);
            AddDiceNrAndRollThenAddToList(m_dice_3, 3);
            AddDiceNrAndRollThenAddToList(m_dice_4, 4);
            AddDiceNrAndRollThenAddToList(m_dice_5, 5);
            AddDiceNrAndRollThenAddToList(m_dice_6, 6);
            return m_diceList;
        }

        public bool Save(model.Dice a_dice)
        {
            m_savedDiceList.Add(a_dice);
            m_diceList.Remove(a_dice);

            bool diceIsSaved = m_savedDiceList.Any(dice => dice.Dicenumber == a_dice.Dicenumber);
            bool diceIsRemoved = m_diceList.Any(dice => dice.Dicenumber != a_dice.Dicenumber);

            if (diceIsSaved && diceIsRemoved)
            {
                return true;
            }

            return false;
        }

        internal void AddDiceNrAndRollThenAddToList(model.Dice a_dice, int a_diceNumber)
        {
            if (a_diceNumber < 1 || a_diceNumber > 6)
            {
                throw new ArgumentOutOfRangeException();
            }

            a_dice.Dicenumber = (Dices)a_diceNumber;
            a_dice.Roll();
            m_diceList.Add(a_dice);
        }
    }
}
