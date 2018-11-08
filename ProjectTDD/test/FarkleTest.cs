using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ProjectTDD.test
{
    public class FarkleTest
    {
        private model.Farkle m_farkle;

        public FarkleTest()
        {
            m_farkle = new model.Farkle();
        }

        [Fact]
        public void Play_ListWith6Dices_return6()
        {
            List<model.Dice> diceList = m_farkle.Play();
            int actual = diceList.Count;
            int expected = 6;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Play_GetValueFromDice1_InRange1to6()
        {
            List<model.Dice> diceList = m_farkle.Play();

            int actual = diceList.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_1).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice2_InRange1to6()
        {
            List<model.Dice> diceList = m_farkle.Play();

            int actual = diceList.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_2).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice3_InRange1to6()
        {
            List<model.Dice> diceList = m_farkle.Play();

            int actual = diceList.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_3).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice4_InRange1to6()
        {
            List<model.Dice> diceList = m_farkle.Play();

            int actual = diceList.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_4).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice5_InRange1to6()
        {
            List<model.Dice> diceList = m_farkle.Play();

            int actual = diceList.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_5).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice6_InRange1to6()
        {
            List<model.Dice> diceList = m_farkle.Play();

            int actual = diceList.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_6).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void AddDiceNumberRollDiceAddToList_UseWrongArgumentInt8_ThrowsArgumentOutOfRangeException()
        {
            model.Dice dice = new model.Dice();
            int outOfRangeNumber = 8;

            Assert.Throws<ArgumentOutOfRangeException>(() => m_farkle.AddDiceNumberRollDiceAddToList(dice, outOfRangeNumber));
        }
    }
}
