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
        public void Show_ListWith6Dices_return6()
        {
            List<model.Dice> input = sut.Show();
            int actual = input.Count;
            int expected = 6;

            Assert.Equal(expected, actual);
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
        public void Roll_WithEmptyList_ThrowsEmptyDiceListException()
        {
            List<model.Dice> diceList = sut.Show();
            List<model.Dice> toBeSaved = new List<model.Dice>();

            // IMPORTANT! Done to avoid index issues.
            foreach (model.Dice d in diceList)
            {
                toBeSaved.Add(d);
            }
            // Save all dices.
            for (int i = 0; i < toBeSaved.Count; i++)
            {
                sut.Save(toBeSaved[i]);
            }
            // Call roll with an empty list.
            Assert.Throws<model.exception.EmptyDiceListException>(() => sut.Roll());
        }

        [Fact]
        public void NoMoreThan6DicesInPlay_CheckBothLists_true()
        {
            bool success = sut.NoMoreThan6DicesInPlay();
            Assert.True(success);
        }

        [Fact]
        public void Reset_MoveAllDicesFromSavedList_Return0()
        {
            List<model.Dice> diceList = sut.Show();
            List<model.Dice> toBeSaved = new List<model.Dice>();

            // IMPORTANT! Done to avoid index issues.
            foreach (model.Dice d in diceList)
            {
                toBeSaved.Add(d);
            }
            // Save all dices.
            for (int i = 0; i < toBeSaved.Count; i++)
            {
                sut.Save(toBeSaved[i]);
            }
            // Move all saved dices back to roll list.
            sut.Reset();

            int actual = sut.ShowSaved().Count;
            int expected = 0;
            Assert.Equal(expected, actual);
        }
    }
}
