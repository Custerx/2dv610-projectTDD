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
        public void GetValue_BeforeDiceRoll_Return0()
        {
            model.Dice.DiceValue actual = sut.GetValue();
            model.Dice.DiceValue expected = 0;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValue_RollDice_ReturnEnumDiceValueInRangeOneToSix()
        {
            sut.Roll();
            model.Dice.DiceValue actual = sut.GetValue();

            model.Dice.DiceValue expectedLow = model.Dice.DiceValue.One;
            model.Dice.DiceValue expectedHigh = model.Dice.DiceValue.Six;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }
    }
}
