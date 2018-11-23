using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Xunit.Sdk;
using ProjectTDD.model;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;

namespace ProjectTDD.test
{
    public class FarkleTest
    {
        private Mock<view.IView> fake_IView;
        private controller.Farkle sut;
        private Mock<model.Dice> fake_dice;
        private Mock<IPlayer> fake_player;

        public FarkleTest()
        {
            fake_dice = new Mock<model.Dice>();
            fake_dice_setup();
            fake_player = new Mock<IPlayer>();
            fake_player_setup();
            fake_IView = new Mock<view.IView>();
            fake_IView_setup();
            sut = new controller.Farkle(fake_IView.Object);
        }

        [Fact]
        public void Start_Should_Call_GetAmountOfPlayers()
        {
            sut.Start(false);
            fake_IView.Verify(mock => mock.GetAmountOfPlayers(false), Times.Once());
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
        public void Start_InputPlayerListWith3Players_Should_Call_PlayerAction3Times()
        {
            sut.Start(false);
            fake_IView.Verify(mock => mock.PlayerAction(It.IsAny<bool>()), Times.Exactly(3));
        }

        [Fact]
        public void Start_Should_Call_DisplayRolledDices()
        {
            sut.Start(false);
            fake_IView.Verify(mock => mock.DisplayRolledDices(It.IsAny<string>(), It.IsAny<List<model.Dice>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Between(2, 8, Range.Inclusive));
        }

        [Fact]
        public void Play_Should_Call_Roll1Time()
        {
            sut.Play(FakePlayerList(), false);
            fake_player.Verify(mock => mock.Roll(), Times.Once());
        }

        [Fact]
        public void Play_Should_Call_UpdateScore1Time()
        {
            sut.Play(FakePlayerList(), false);
            fake_player.Verify(mock => mock.UpdateTotalScore(), Times.Once());
        }

        [Fact]
        public void Play_Should_Call_DisplayRolledDices2Times()
        {
            sut.Play(FakePlayerList(), false);
            fake_IView.Verify(mock => mock.DisplayRolledDices(It.IsAny<string>(), It.IsAny<List<model.Dice>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public void Play_PlayerActionReturnEnumActionSave_Should_Call_PlayerGetHand2Times()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<bool>())).Returns(view.FarkleView.Action.Save);

            var sut_local = new controller.Farkle(fake_IView_local.Object);

            sut_local.Play(FakePlayerList(), false);

            fake_player.Verify(mock => mock.GetHand(), Times.Exactly(2));
        }

        [Fact]
        public void Play_PlayerActionReturnEnumActionQuit_Should_Throw_ValidateQuitException()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<bool>())).Returns(view.FarkleView.Action.Quit);

            var sut_local = new controller.Farkle(fake_IView_local.Object);

            Assert.Throws<model.exception.ValidateQuitException>(() => sut_local.Play(FakePlayerList(), false));
        }

        [Fact]
        public void Play_PlayerActionReturnEnumActionNewGame_Should_Throw_ValidateNewGameException()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<bool>())).Returns(view.FarkleView.Action.NewGame);

            var sut_local = new controller.Farkle(fake_IView_local.Object);

            Assert.Throws<model.exception.ValidateNewGameException>(() => sut_local.Play(FakePlayerList(), false));
        }

        [Fact]
        public void Play_PlayerActionReturnEnumActionSave_Should_Call_PlayerIsPlayerWinner1Time()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<bool>())).Returns(view.FarkleView.Action.Roll);

            var sut_local = new controller.Farkle(fake_IView_local.Object);

            sut_local.Play(FakePlayerList(), false);

            fake_player.Verify(mock => mock.IsPlayerWinner(), Times.Once());
        }

        [Fact]
        public void Play_PlayerActionReturnEnumActionSave_Should_Call_DisplayWinner()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<bool>())).Returns(view.FarkleView.Action.Roll);
            fake_IView_local.Setup(mock => mock.DisplayWinner(It.IsAny<string>(), It.IsAny<int>())).Verifiable();

            var sut_local = new controller.Farkle(fake_IView_local.Object);

            sut_local.Play(FakePlayerList(), false);

            fake_IView_local.Verify(mock => mock.DisplayWinner(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Action_WhenPlayerHitQ_ReturnFalse()
        {
            bool fail = sut.Action(fake_player.Object, "q", true);
            Assert.False(fail);
        }

        [Fact]
        public void AddName_Should_Call_GetPlayername3Times()
        {
            List<model.IPlayer> playerList = sut.CreatePlayer(3);
            sut.AddName(playerList);
            fake_IView.Verify(mock => mock.GetPlayername(true), Times.Exactly(3));
        }

        private void fake_IView_setup()
        {
            fake_IView.Setup(mock => mock.GetAmountOfPlayers(false)).Returns(3).Verifiable();
            fake_IView.Setup(mock => mock.DisplayRolledDices("Rogge", FakeDiceList(), 300, 2400)).Verifiable();
            fake_IView.Setup(mock => mock.PlayerAction(It.IsAny<bool>())).Returns(view.FarkleView.Action.Roll).Verifiable();
            fake_IView.Setup(mock => mock.DisplayWinner(It.IsAny<string>(), It.IsAny<int>())).Verifiable();
            fake_IView.Setup(mock => mock.GetPlayername(It.IsAny<bool>())).Verifiable();
        }

        private void fake_dice_setup()
        {
            fake_dice.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three).Verifiable();
            fake_dice.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1).Verifiable();
        }

        private void fake_player_setup()
        {
            fake_player.Setup(mock => mock.GetHand()).Returns(FakeDiceList()).Verifiable();
            fake_player.Setup(mock => mock.Roll()).Verifiable();
            fake_player.Setup(mock => mock.UpdateTotalScore()).Verifiable();
            fake_player.Setup(mock => mock.IsPlayerWinner()).Returns(true).Verifiable();
        }

        private List<model.Dice> FakeDiceList()
        {
            List<model.Dice> fakedicelist = new List<model.Dice>();
            fakedicelist.Add(fake_dice.Object);
            return fakedicelist;
        }

        private List<IPlayer> FakePlayerList()
        {
            List<IPlayer> fakelist = new List<IPlayer>();
            fakelist.Add(fake_player.Object);
            return fakelist;
        }
    }
}
