using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

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
            m_hand.Save(a_dice);
        }

        public virtual int CalculateScore()
        {
            List<int> scoreList = new List<int>();
            int ones = 0;
            int twos = 0;
            int threes = 0;
            int fours = 0;
            int fives = 0;
            int sixes = 0;

            foreach(model.Dice dice in GetHand())
            {
                scoreList.Add(dice.GetValue());
            }

            foreach(int value in scoreList)
            {
                if(value == 1)
                {
                    ones++;
                }

                if (value == 2)
                {
                    twos++;
                }

                if (value == 3)
                {
                    threes++;
                }

                if (value == 4)
                {
                    fours++;
                }

                if (value == 5)
                {
                    fives++;
                }

                if (value == 6)
                {
                    sixes++;
                }
            }

            return PointsForCombinations(ones, twos, threes, fours, fives, sixes);
        }

        // https://www.dicegamedepot.com/farkle-rules/
        private int PointsForCombinations(int a_ones, int a_twos, int a_threes, int a_fours, int a_fives, int a_sixes)
        {
            int score = 0;
            int pairs = 0;

            // Score for 1 and 5.
            if (a_ones == 1)
            {
                score += 100;
            }

            if (a_fives == 1)
            {
                score += 50;
            }

            // Score for 3 of a kind.
            if (a_ones == 3)
            {
                score += 1000;
            }

            if (a_twos == 3)
            {
                score += 200;
            }

            if (a_threes == 3)
            {
                score += 300;
            }

            if (a_fours == 3)
            {
                score += 400;
            }

            if (a_fives == 3)
            {
                score += 500;
            }

            if (a_sixes == 3)
            {
                score += 600;
            }

            // Score for 1 2 3 4 5 6
            if (a_ones == 1 && a_twos == 1 && a_threes == 1 && a_fours == 1 && a_fives == 1 && a_sixes == 1)
            {
                score += 3000;
            }

            // Score for 3 pairs.
            if (a_ones == 2)
            {
                pairs += 1;
            }

            if (a_twos == 2)
            {
                pairs += 1;
            }

            if (a_threes == 2)
            {
                pairs += 1;
            }

            if (a_fours == 2)
            {
                pairs += 1;
            }

            if (a_fives == 2)
            {
                pairs += 1;
            }

            if (a_sixes == 2)
            {
                pairs += 1;
            }

            if (pairs == 3)
            {
                score += 1500;
            }

            return score;
        }
    }
}
