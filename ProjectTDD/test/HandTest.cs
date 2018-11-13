using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ProjectTDD.test
{
    public class HandTest
    {
        private model.Hand sut;

        public HandTest()
        {
            sut = new model.Hand();
        }

        [Fact]
        public void Show_GetListWith0Dices_EmptyList()
        {
            List<model.Dice> input = sut.Show();
            int actual = input.Count;
            int expected = 0;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Show_ListWith6Dices_return6()
        {
            sut.Play();
            List<model.Dice> input = sut.Show();
            int actual = input.Count;
            int expected = 6;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Play_GetValueFromDice1_InRange1to6()
        {
            sut.Play();
            List<model.Dice> input = sut.Show();

            int actual = input.First(dice => dice.Dicenumber == model.Hand.Dices.Dice_1).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice2_InRange1to6()
        {
            sut.Play();
            List<model.Dice> input = sut.Show();

            int actual = input.First(dice => dice.Dicenumber == model.Hand.Dices.Dice_2).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice3_InRange1to6()
        {
            sut.Play();
            List<model.Dice> input = sut.Show();

            int actual = input.First(dice => dice.Dicenumber == model.Hand.Dices.Dice_3).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice4_InRange1to6()
        {
            sut.Play();
            List<model.Dice> input = sut.Show();

            int actual = input.First(dice => dice.Dicenumber == model.Hand.Dices.Dice_4).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice5_InRange1to6()
        {
            sut.Play();
            List<model.Dice> input = sut.Show();

            int actual = input.First(dice => dice.Dicenumber == model.Hand.Dices.Dice_5).GetValue();
            int expectedLow = 1;
            int expectedHigh = 6;

            Assert.InRange(actual, expectedLow, expectedHigh);
        }

        [Fact]
        public void Play_GetValueFromDice6_InRange1to6()
        {
            sut.Play();
            List<model.Dice> input = sut.Show();

            int actual = input.First(dice => dice.Dicenumber == model.Hand.Dices.Dice_6).GetValue();
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

        [Fact]
        public void Save_Dice1_Success()
        {
            sut.Play();
            List<model.Dice> input = sut.Show();
            model.Dice dice1 = input.First(dice => dice.Dicenumber == model.Hand.Dices.Dice_1);

            bool success = sut.Save(dice1);

            Assert.True(success);
        }

        [Fact]
        public void ShowSaved_GetListWith0Dices_EmptyList()
        {
            List<model.Dice> input = sut.ShowSaved();
            int actual = input.Count;
            int expected = 0;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RollNonSavedDices_RollRemainingDices_void()
        {
            bool success = sut.RollNonSavedDices();
            Assert.False(success);
        }
    }
}
