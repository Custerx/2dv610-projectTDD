using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Xunit.Sdk;
using ProjectTDD.model;

namespace ProjectTDD.test
{
    public class FarkleTest
    {
        private Mock<view.FarkleView> mock_farkleview;
        private controller.Farkle sut;
        private Mock<model.Dice> fake_dice;

        public FarkleTest()
        {
            fake_dice = new Mock<model.Dice>();
            fake_dice_setup();
            mock_farkleview = new Mock<view.FarkleView>();
            mock_farkleview_setup();
            sut = new controller.Farkle(mock_farkleview.Object);
        }

        [Fact]
        public void Start_Should_Call_GetAmountOfPlayers()
        {
            sut.Start();
            mock_farkleview.Verify(mock => mock.GetAmountOfPlayers(false), Times.Once());
        }

        [Fact]
        public void CreatePlayer_Input3_ReturnsListWith3Players()
        {
            List<model.IPlayer> playerList = sut.CreatePlayer(3);
            int actual = playerList.Count;
            int expected = 3;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Play_InputPlayerListWith3Players_VoidSmokeSignal()
        {
            List<model.IPlayer> playerList = sut.CreatePlayer(3);
            sut.Play(playerList);
        }

        [Fact]
        public void Start_Should_Call_DisplayRolledDices()
        {
            sut.Start();
            mock_farkleview.Verify(mock => mock.DisplayRolledDices(It.IsAny<string>(), It.IsAny<List<model.Dice>>(), It.IsAny<int>()), Times.Once());
        }

        private void mock_farkleview_setup()
        {
            mock_farkleview.Setup(mock => mock.GetAmountOfPlayers(false)).Returns(3).Verifiable();
            mock_farkleview.Setup(mock => mock.DisplayRolledDices("Rogge", FakeDiceList(), 300)).Verifiable();
        }

        private void fake_dice_setup()
        {
            fake_dice.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three).Verifiable();
            fake_dice.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1).Verifiable();
        }

        private List<model.Dice> FakeDiceList()
        {
            List<model.Dice> fakedicelist = new List<model.Dice>();
            fakedicelist.Add(fake_dice.Object);
            return fakedicelist;
        }
    }
}
