using System;
using Xunit;

namespace ProjectTDD.test
{
    public class DiceTest
    {
        private model.Dice m_dice;

        public DiceTest()
        {
            m_dice = new model.Dice();
        }

        [Fact]
        public void GetValue_RandomValue_InRange1to6()
        {
            int actual = m_dice.GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void GetValue_BeforeDiceRoll_Return0()
        {
            int actual = m_dice.GetValue();
            int expected = 0;

            Assert.Equal(expected, actual);
        }
    }
}
