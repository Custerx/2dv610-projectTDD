using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ProjectTDD.test
{
    public class FarkleTest
    {
        private model.Farkle sut;

        public FarkleTest()
        {
            sut = new model.Farkle();
        }

        [Fact]
        public void Play_ListWith6Dices_return6()
        {
            List<model.Dice> input = sut.Play();
            int actual = input.Count;
            int expected = 6;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Play_GetValueFromDice1_InRange1to6()
        {
            List<model.Dice> input = sut.Play();

            int actual = input.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_1).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice2_InRange1to6()
        {
            List<model.Dice> input = sut.Play();

            int actual = input.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_2).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice3_InRange1to6()
        {
            List<model.Dice> input = sut.Play();

            int actual = input.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_3).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice4_InRange1to6()
        {
            List<model.Dice> input = sut.Play();

            int actual = input.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_4).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice5_InRange1to6()
        {
            List<model.Dice> input = sut.Play();

            int actual = input.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_5).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice6_InRange1to6()
        {
            List<model.Dice> input = sut.Play();

            int actual = input.First(dice => dice.Dicenumber == model.Farkle.Dices.Dice_6).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void AddDiceNrAndRollThenAddToList_UseWrongArgumentInt7_ThrowsArgumentOutOfRangeException()
        {
            model.Dice dice = new model.Dice();
            int input = 7;

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.AddDiceNrAndRollThenAddToList(dice, input));
        }

        [Fact]
        public void AddDiceNrAndRollThenAddToList_UseWrongArgumentInt0_ThrowsArgumentOutOfRangeException()
        {
            model.Dice dice = new model.Dice();
            int input = 0;

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.AddDiceNrAndRollThenAddToList(dice, input));
        }
    }
}
