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
    }
}
