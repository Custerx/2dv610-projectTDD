﻿using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ProjectTDD.test
{
    public class FarkleViewTest
    {
        private Mock<model.Dice> fake_dice;
        private view.IView sut;
        private readonly ITestOutputHelper m_output; // Capturing output.
        private Mock<model.IPlayer> mock_player;

        public FarkleViewTest(ITestOutputHelper a_output)
        {
            fake_dice = new Mock<model.Dice>();
            fake_dice_setup();
            mock_player = new Mock<model.IPlayer>();
            mock_player_setup();
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
            SimulateOutputFromDisplayRolledDices("Rogge", fake_dice_list(), 300, 3000);
        }

        [Fact]
        public void GetAmountOfPlayers_SetArgumentToTrue_ReturnsInt3()
        {
            int actual = sut.GetAmountOfPlayers(true);
            int expected = 3;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAction_PressKeyQ_False()
        {
            string input = "q";
            bool fail = sut.GetAction(mock_player.Object, input, true);
            Assert.False(fail);
        }

        [Fact]
        public void GetAction_PressKeyR_Should_Call_Roll()
        {
            string input = "r";
            sut.GetAction(mock_player.Object, input, true);
            mock_player.Verify(mock => mock.Roll(), Times.Once());
        }

        [Fact]
        public void GetAction_PressKeyS_Should_Call_Save()
        {
            string input = "s";
            sut.GetAction(mock_player.Object, input, true);
            mock_player.Verify(mock => mock.Save(fake_dice.Object), Times.Once());
        }

        [Fact]
        public void GetAction_PressKeyS_Should_Call_GetHand()
        {
            string input = "s";
            Assert.Throws<model.exception.InvalidStringArgumentException>(() => sut.GetAction(mock_player.Object, input));
        }

        [Fact]
        public void GetAction_UseWrongArgumentStringE_ThrowsArgumentException()
        {
            string input = "e";
            Assert.Throws<model.exception.TestStringArgumentException>(() => sut.GetAction(mock_player.Object, input, true));
        }

        [Fact]
        public void DisplayGameKeys_CompareWithConsoleOuput_Equal()
        {
            // Part of this code is inspired by code found in 2DV610 slack channel.
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut.DisplayGameKeys();
                string expected = string.Format("\nStart new game: [1]. Roll non-saved dice(s): [2]. Save dice(s): [3]. Quit game: [4].\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void PlayerAction_SendArgumentTrue_ReturnsActionSave()
        {
            bool test = true;
            view.FarkleView.Action actual = sut.PlayerAction(test);
            view.FarkleView.Action expected = view.FarkleView.Action.Save;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DisplayRolledDices_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut.DisplayRolledDices("Rogge", fake_dice_list(), 300, 3000);
                string expected = string.Format("Rogge Rolled: \nDice_1 : Five\nDice_2 : Three\nScore: 300\nTotal-score: 3000\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        private void mock_player_setup()
        {
            mock_player.Setup(mock => mock.Roll()).Verifiable();
            mock_player.Setup(mock => mock.Save(fake_dice.Object)).Verifiable();
            mock_player.Setup(mock => mock.GetHand()).Returns(fake_dice_list()).Verifiable();
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

        private void SimulateOutputFromDisplayRolledDices(String a_player, List<model.Dice> a_hand, int a_score, int a_totalScore)
        {
            m_output.WriteLine("{0} Rolled: ", a_player);
            foreach (model.Dice d in a_hand)
            {
                SimulateOutputFromDisplayDice(d);
            }
            m_output.WriteLine("Score: {0}", a_score);
            m_output.WriteLine("");
            m_output.WriteLine("Total-score: {0}", a_totalScore);
            m_output.WriteLine("");
        }
    }
}
