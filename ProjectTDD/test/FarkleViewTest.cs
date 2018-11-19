using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ProjectTDD.test
{
    public class FarkleViewTest
    {
        private Mock<model.Dice> fake_dice;
        private view.FarkleView sut;
        private readonly ITestOutputHelper m_output; // Capturing output.

        public FarkleViewTest(ITestOutputHelper a_output)
        {
            fake_dice = new Mock<model.Dice>();
            fake_dice_setup();
            sut = new view.FarkleView();
            m_output = a_output; // https://xunit.github.io/docs/capturing-output
        }

        [Fact]
        public void DisplayDice_ArgumentFakeDice_OutputDice_1Value5()
        {
            // This is a copy of FarkleView.DisplayDice(model.Dice a_dice)
            SimulateOutputFromDisplayDice(fake_dice.Object);
        }

        [Fact]
        public void DisplayRolledDices_ArgumentFakeDiceList_Dice_1Value5_Dice_2Value3()
        {
            // This is a copy of FarkleView.DisplayRolledDices(string a_player, List<model.Dice> a_hand, int a_score)
            SimulateOutputFromDisplayRolledDices("Rogge", fake_dice_list(), 300);
        }

        [Fact]
        public void WantsToRollDice_NotPressingCharacterR_False()
        {
            bool success = sut.WantsToRollDice();
            Assert.False(success);
        }

        [Fact]
        public void GetAmountOfPlayers_UserInput3Players_ReturnsInt3()
        {
            int actual = sut.GetAmountOfPlayers();
            int expected = 3;
            Assert.Equal(expected, actual);
        }

        private void fake_dice_setup()
        {
            fake_dice.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Five);
            fake_dice.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);
        }

        private List<model.Dice> fake_dice_list()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice2 = new Mock<model.Dice>();
            fake_dice2.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice2.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            dicelist.Add(fake_dice.Object);
            dicelist.Add(fake_dice2.Object);
            return dicelist;
        }

        private void SimulateOutputFromDisplayDice(model.Dice a_fakedice)
        {
            m_output.WriteLine("{0} : {1}", a_fakedice.Dicenumber, a_fakedice.GetValue());
        }

        private void SimulateOutputFromDisplayRolledDices(String a_player, List<model.Dice> a_hand, int a_score)
        {
            m_output.WriteLine("{0} Rolled: ", a_player);
            foreach (model.Dice d in a_hand)
            {
                SimulateOutputFromDisplayDice(d);
            }
            m_output.WriteLine("Score: {0}", a_score);
            m_output.WriteLine("");
        }
    }
}
