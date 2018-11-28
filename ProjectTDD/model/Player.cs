using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;
using ProjectTDD.model.exception;

namespace ProjectTDD.model
{
    public class Player : IPlayer
    {
        const int WinningScore = 10000;
        private IHand m_hand;
        private int m_score;
        private string m_playername;

        public Player(IHand a_hand)
        {
            m_hand = a_hand;
        }

        public enum DiceState
        {
            Saved,
            Rolled
        }

        public void SetPlayername(string a_name)
        {
            if(a_name.Length < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            m_playername = a_name;
        }

        public string GetPlayername()
        {
            return m_playername;
        }

        public List<model.Dice> GetHand()
        {
            return m_hand.Show();
        }

        public List<model.Dice> GetSavedHand()
        {
            return m_hand.ShowSaved();
        }

        public void Roll()
        {
            m_hand.Roll();
        }

        public void Save(model.Dice a_dice)
        {
            m_hand.Save(a_dice);
        }

        public int GetTotalScore()
        {
            return m_score;
        }

        public bool IsPlayerWinner()
        {
            return m_score >= WinningScore;
        }

        public bool IsMoreDicesToRoll()
        {
            return m_hand.MoreDicesToRoll();
        }

        public int CalculateScore()
        {
            int savedDicesScore = CalculateDiceStateScore(DiceState.Saved);
            int rolledDicesScore = CalculateDiceStateScore(DiceState.Rolled);

            if (IsFarkle(savedDicesScore, rolledDicesScore))
            {
                return 0;
            }

            return savedDicesScore + rolledDicesScore;
        }


        public void UpdateTotalScore()
        {
            int savedDicesScore = CalculateDiceStateScore(DiceState.Saved);
            int rolledDicesScore = CalculateDiceStateScore(DiceState.Rolled);
            m_hand.Reset();
            m_score += savedDicesScore + rolledDicesScore;
        }

        public bool IsFarkle(int a_savedDiceScore, int a_rolledDiceScore)
        {
            return (a_savedDiceScore >= 50 && a_rolledDiceScore == 0);
        }

        private int CalculateDiceStateScore(DiceState diceState)
        {
            List<model.Dice.DiceValue> scoreList = new List<model.Dice.DiceValue>();
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

            foreach (model.Dice.DiceValue value in scoreList)
            {
                if(value == model.Dice.DiceValue.One)
                {
                    ones++;
                }

                if (value == model.Dice.DiceValue.Two)
                {
                    twos++;
                }

                if (value == model.Dice.DiceValue.Three)
                {
                    threes++;
                }

                if (value == model.Dice.DiceValue.Four)
                {
                    fours++;
                }

                if (value == model.Dice.DiceValue.Five)
                {
                    fives++;
                }

                if (value == model.Dice.DiceValue.Six)
                {
                    sixes++;
                }
            }

            return MaxPointCombination(ones, twos, threes, fours, fives, sixes);
        }

        // https://www.dicegamedepot.com/farkle-rules/
        private int MaxPointCombination(int a_ones, int a_twos, int a_threes, int a_fours, int a_fives, int a_sixes)
        {
            const int One = 1;
            const int Two = 2;
            const int Three = 3;
            const int Four = 4;
            const int Six = 6;

            int score = 0;
            int pairs = 0;

            // Score for 3 of a kind.
            if (a_ones == Three)
            {
                score += 1000;
            }

            if (a_twos == Three)
            {
                score += 200;
            }

            if (a_threes == Three)
            {
                score += 300;
            }

            if (a_fours == Three)
            {
                score += 400;
            }

            if (a_fives == Three)
            {
                score += 500;
            }

            if (a_sixes == Three)
            {
                score += 600;
            }

            // Score for 1 2 3 4 5 6
            if (a_ones == One && a_twos == One && a_threes == One && a_fours == One && a_fives == One && a_sixes == One)
            {
                score += 3000;
            } else
            {
                // Score for 1 and 5.
                if (a_ones == One)
                {
                    score += 100;
                }

                if (a_fives == One)
                {
                    score += 50;
                }
            }

            // Score for 3 pairs.
            if (a_ones == Two)
            {
                pairs += 1;
            }

            if (a_twos == Two)
            {
                pairs += 1;
            }

            if (a_threes == Two)
            {
                pairs += 1;
            }

            if (a_fours == Two)
            {
                pairs += 1;
            }

            if (a_fives == Two)
            {
                pairs += 1;
            }

            if (a_sixes == Two)
            {
                pairs += 1;
            }

            if (pairs == Three)
            {
                score += 1500;
            }

            // Score for 4 of a kind and 1 pair.
            if (a_ones == Four || a_twos == Four || a_threes == Four || a_fours == Four || a_fives == Four || a_sixes == Four)
            {
                if (pairs == One)
                {
                    score += 1500;
                }
            }

            // Score for pair of 1's and pair if 5's when 3 pair score combination not been hit.
            if (pairs < Three)
            {
                if (a_ones == Two)
                {
                    score += 100 * 2;
                }

                if (a_fives == Two)
                {
                    
                }
            }

            // Score for 6 of a kind.
            if (a_ones == Six || a_twos == Six || a_threes == Six || a_fours == Six || a_fives == Six || a_sixes == Six)
            {
                score += 3000;
            }

            return score;
        }
    }
}
