using System;
using Xunit;

namespace ProjectTDD.test
{
    public class DiceTest
    {
        private model.Dice sut;

        public DiceTest()
        {
            sut = new model.Dice();
        }

        [Fact]
        public void GetValue_Roll_InRange1to6()
        {
            sut.Roll();

            int actual = sut.GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void GetValue_BeforeDiceRoll_Return0()
        {
            int actual = sut.GetValue();
            int expected = 0;

            Assert.Equal(expected, actual);
        }
    }
}
