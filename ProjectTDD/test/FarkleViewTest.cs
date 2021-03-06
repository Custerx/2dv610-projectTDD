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
        private Mock<model.IPlayer> mock_player;
        private view.FarkleView sut_farkleview; // Not using interface so I can reach internal function.
        private Mock<model.console.readline.IConsoleReadline> mock_console_readline;

        public FarkleViewTest()
        {
            fake_dice = new Mock<model.Dice>();
            fake_dice_setup();
            mock_player = new Mock<model.IPlayer>();
            mock_player_setup();
            mock_console_readline = new Mock<model.console.readline.IConsoleReadline>();
            mock_console_readline_setup();
            sut = new view.FarkleView(mock_console_readline.Object);
            sut_farkleview = new view.FarkleView(mock_console_readline.Object);
        }

        [Fact]
        public void GetAmountOfPlayers_SetArgumentToString3_ReturnsInt3()
        {
            int actual = sut.GetAmountOfPlayers("3");
            int expected = 3;
            Assert.Equal(expected, actual);
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
        public void GetDiceToSave_ArgumentString3_ReturnsEnumDicesDice_3()
        {
            model.Hand.Dices actual = sut.GetDiceToSave("3");
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
                string expected = string.Format("\nSave Dices. Dice_1 = [1], Dice_2 = [2], Dice_3 = [3], Dice_4 = [4]\n Dice_5 = [5], Dice_6 = [6], DONE = [7]\n\r\n");
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
            Assert.Throws<ApplicationException>(() => sut_farkleview.PlayerActionTestable("0"));
        }

        [Fact]
        public void PlayerActionErrorMessage_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut_farkleview.PlayerActionErrorMessage();
                string expected = string.Format("\nError! Your choice must contain a number between 1 and 4.\n\r\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void GetPlayerNameTestable_UseWrongArgumentEmptyString_ThrowsApplicationException()
        {
            Assert.Throws<ApplicationException>(() => sut_farkleview.GetPlayerNameTestable(""));
        }

        [Fact]
        public void GetPlayerNameIntroMessage_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut_farkleview.GetPlayerNameIntroMessage();
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
                sut_farkleview.GetPlayerNameErrorMessage();
                string expected = string.Format("\nError! Your name must contain atleast 1 character.\n\r\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void GetAmountOfPlayersTestable_UseWrongArgumentEmptyString_ThrowsApplicationException()
        {
            Assert.Throws<ApplicationException>(() => sut_farkleview.GetAmountOfPlayersTestable("1"));
        }

        [Fact]
        public void GetAmountOfPlayersIntroMessage_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut_farkleview.GetAmountOfPlayersIntroMessage();
                string expected = string.Format("Chose amount of players. Between [2] and [8] :\r\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void GetAmountOfPlayersErrorMessage_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut_farkleview.GetAmountOfPlayersErrorMessage();
                string expected = string.Format("\nError! Your choice must contain a number between 2 and 8.\n\r\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void GetDiceToSaveTestable_UseWrongArgumentString0_ThrowsApplicationException()
        {
            Assert.Throws<ApplicationException>(() => sut_farkleview.GetDiceToSaveTestable("0"));
        }

        [Fact]
        public void GetDiceToSaveErrorMessage_CompareWithConsoleOuput_Equal()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut_farkleview.GetDiceToSaveErrorMessage();
                string expected = string.Format("\nError! Your choice must contain a number between 1 and 7.\n\r\n");
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void GetAmountOfPlayers_NullAsArgument_ReturnInt2()
        {
            int actual = sut.GetAmountOfPlayers(null);
            int expected = 2; // Set by mock_console_readline_setup()
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDiceToSave_NullAsArgument_ReturnModelHandDices_2()
        {
            model.Hand.Dices actual = sut.GetDiceToSave(null);
            model.Hand.Dices expected = model.Hand.Dices.Dice_2; // Set by mock_console_readline_setup()
            Assert.Equal(expected, actual);
        }

        private void mock_player_setup()
        {
            mock_player.Setup(mock => mock.Roll()).Verifiable();
            mock_player.Setup(mock => mock.Save(fake_dice.Object)).Verifiable();
            mock_player.Setup(mock => mock.GetHand()).Returns(fake_dice_list()).Verifiable();
        }

        private void mock_console_readline_setup()
        {
            mock_console_readline.Setup(mock => mock.ReadLine()).Returns("2").Verifiable();
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
    }
}
