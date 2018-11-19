using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;
using ProjectTDD.model.exception;

namespace ProjectTDD.model
{
    public class Player
    {
        private model.Hand m_hand;
        private int m_score;

        public Player(model.Hand a_hand)
        {
            m_hand = a_hand;
        }

        public enum DiceState
        {
            Saved,
            Rolled
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

        public virtual int GetTotalScore()
        {
            return m_score;
        }

        public virtual int CalculateScore()
        {
            int savedDicesScore = CalculateDiceStateScore(DiceState.Saved);
            int rolledDicesScore = CalculateDiceStateScore(DiceState.Rolled);

            if (IsFarkle(savedDicesScore, rolledDicesScore))
            {
                return 0;
            }

            UpdateScore(savedDicesScore + rolledDicesScore);
            return savedDicesScore + rolledDicesScore;
        }

        internal virtual bool IsFarkle(int a_savedDiceScore, int a_rolledDiceScore)
        {
            return (a_savedDiceScore > 0 && a_rolledDiceScore == 0);
        }

        private int CalculateDiceStateScore(DiceState diceState)
        {
            List<int> scoreList = new List<int>();
            int ones = 0;
            int twos = 0;
            int threes = 0;
            int fours = 0;
            int fives = 0;
            int sixes = 0;

            if (diceState == DiceState.Rolled)
            {
                foreach (model.Dice dice in GetHand())
                {
                    scoreList.Add(dice.GetValue());
                }
            }

            if (diceState == DiceState.Saved)
            {
                foreach (model.Dice dice in GetSavedHand())
                {
                    scoreList.Add(dice.GetValue());
                }
            }

            foreach (int value in scoreList)
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

            return MaxPointCombination(ones, twos, threes, fours, fives, sixes);
        }

        // https://www.dicegamedepot.com/farkle-rules/
        private int MaxPointCombination(int a_ones, int a_twos, int a_threes, int a_fours, int a_fives, int a_sixes)
        {
            int score = 0;
            int pairs = 0;

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

            // Score for 4 of a kind and 1 pair.
            if (a_ones == 4 || a_twos == 4 || a_threes == 4 || a_fours == 4 || a_fives == 4 || a_sixes == 4)
            {
                if (pairs == 1)
                {
                    score += 1500;
                }
            }

            // Score for 1 and 5.
            if (a_ones == 1)
            {
                score += 100;
            }

            if (a_fives == 1)
            {
                score += 50;
            }

            // Score for pair of 1's and pair if 5's when 3 pair score combination not been hit.
            if (pairs < 3)
            {
                if (a_ones == 2)
                {
                    score += 100 * 2;
                }

                if (a_fives == 2)
                {
                    score += 50 * 2;
                }
            }

            // Score for 6 of a kind.
            if (a_ones == 6 || a_twos == 6 || a_threes == 6 || a_fours == 6 || a_fives == 6 || a_sixes == 6)
            {
                score += 3000;
            }

            return score;
        }

        private void UpdateScore(int a_score)
        {
            m_score += a_score;
        }
    }
}
