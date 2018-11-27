using Moq;
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
                string expected = string.Format("\n Roll = [1], Save dice(s) = [2], New game = [3], Quit game = [4].\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void PlayerAction_SendArgumentString3_ReturnsActionNewGame()
        {
            view.FarkleView.Action actual = sut.PlayerAction("3");
            view.FarkleView.Action expected = view.FarkleView.Action.NewGame;
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

        [Fact]
        public void DisplayWinner_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut.DisplayWinner("Rogge", 10000);
                string expected = string.Format("\nRogge WON!\nWith a total-score of: 10000\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void GetPlayername_ArgumentStringRogge_ReturnsStringRogge()
        {
            string actual = sut.GetPlayername("Rogge");
            string expected = "Rogge";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDiceToSave_ArgumentTrue_ReturnsEnumDicesDice_3()
        {
            model.Hand.Dices actual = sut.GetDiceToSave(true);
            model.Hand.Dices expected = model.Hand.Dices.Dice_3;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DisplaySaveKeys_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut.DisplaySaveKeys();
                string expected = string.Format("\nSave Dices. Dice_1 = [1], Dice_2 = [2], Dice_3 = [3], Dice_4 = [4], Dice_5 = [5], Dice_6 = [6], EXIT = [7]\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void DisplayCannotSaveDiceTwice_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut.DisplayCannotSaveDiceTwice();
                string expected = string.Format("\nError! You cannot save the same dice twice.\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void PlayerActionTestable_UseWrongArgumentString0_ThrowsApplicationException()
        {
            var sut_local = new view.FarkleView(); // Not using interface so I can reach internal function.
            Assert.Throws<ApplicationException>(() => sut_local.PlayerActionTestable("0"));
        }

        [Fact]
        public void PlayerActionErrorMessage_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                var sut_local = new view.FarkleView(); // Not using interface so I can reach internal function.
                sut_local.PlayerActionErrorMessage();
                string expected = string.Format("\nError! Your choice must contain a number between 1 and 4.\n\r\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void GetPlayerNameTestable_UseWrongArgumentEmptyString_ThrowsApplicationException()
        {
            var sut_local = new view.FarkleView(); // Not using interface so I can reach internal function.
            Assert.Throws<ApplicationException>(() => sut_local.GetPlayerNameTestable(""));
        }

        [Fact]
        public void GetPlayerNameIntroMessage_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                var sut_local = new view.FarkleView(); // Not using interface so I can reach internal function.
                sut_local.GetPlayerNameIntroMessage();
                string expected = string.Format("Please type your name: \r\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void GetPlayerNameErrorMessage_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                var sut_local = new view.FarkleView(); // Not using interface so I can reach internal function.
                sut_local.GetPlayerNameErrorMessage();
                string expected = string.Format("\nError! Your name must contain atleast 1 character.\n\r\n");
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
