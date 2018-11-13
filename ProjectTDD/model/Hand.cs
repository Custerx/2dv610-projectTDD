using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using ProjectTDD.model.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Sdk;

namespace ProjectTDD.model
{
    public class Hand
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

        public Hand()
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

        public void Roll()
        {
            if (m_diceList.Count < 1)
            {
                throw new EmptyListException();
            }

            RollNonSavedDices();
        }

        private void RollNonSavedDices()
        {
            foreach (model.Dice dice in m_diceList)
            {
                dice.Roll();
            }
        }

        public List<model.Dice> Show()
        {
            return m_diceList;
        }

        public List<model.Dice> ShowSaved()
        {
            return m_savedDiceList;
        }

        public virtual void Play()
        {
            AddDiceNrAndRollThenAddToList(m_dice_1, 1);
            AddDiceNrAndRollThenAddToList(m_dice_2, 2);
            AddDiceNrAndRollThenAddToList(m_dice_3, 3);
            AddDiceNrAndRollThenAddToList(m_dice_4, 4);
            AddDiceNrAndRollThenAddToList(m_dice_5, 5);
            AddDiceNrAndRollThenAddToList(m_dice_6, 6);
        }

        public void AddDiceNrAndRollThenAddToList(model.Dice a_dice, int a_diceNumber)
        {
            if (a_diceNumber < 1 || a_diceNumber > 6)
            {
                throw new ArgumentOutOfRangeException();
            }

            a_dice.Dicenumber = (Dices)a_diceNumber;
            a_dice.Roll();
            m_diceList.Add(a_dice);
        }

        public bool Save(model.Dice a_dice)
        {
            SaveAndRemoveDice(a_dice);

            if (IsDiceSaved(a_dice) && IsDiceNotRemoved(a_dice) == false)
            {
                return true;
            }

            return false;
        }

        private void SaveAndRemoveDice(model.Dice a_dice)
        {
            m_savedDiceList.Add(a_dice);
            m_diceList.Remove(a_dice);
        }

        private bool IsDiceSaved(model.Dice a_dice)
        {
            return m_savedDiceList.Any(dice => dice.Dicenumber == a_dice.Dicenumber);
        }

        private bool IsDiceNotRemoved(model.Dice a_dice)
        {
            return m_diceList.Any(dice => dice.Dicenumber == a_dice.Dicenumber);
        }
    }
}
