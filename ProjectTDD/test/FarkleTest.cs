﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Xunit.Sdk;
using ProjectTDD.model;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System.Threading.Tasks;

namespace ProjectTDD.test
{
    public class FarkleTest
    {
        private Mock<view.IView> fake_IView;
        private controller.Farkle sut;
        private Mock<model.Dice> fake_dice;
        private Mock<IPlayer> fake_player;
        private Mock<model.task.delay.IAsyncDelay> fake_async_delay;
        private Mock<model.env.exit.IEnvironmentExit> fake_envExit;

        public FarkleTest()
        {
            fake_dice = new Mock<model.Dice>();
            fake_dice_setup();
            fake_player = new Mock<IPlayer>();
            fake_player_setup();
            fake_async_delay = new Mock<model.task.delay.IAsyncDelay>();
            fake_async_delay_setup();
            fake_envExit = new Mock<model.env.exit.IEnvironmentExit>();
            fake_envExit_setup();
            fake_IView = new Mock<view.IView>();
            fake_IView_setup();
            sut = new controller.Farkle(fake_IView.Object, fake_envExit.Object);
        }

        [Fact]
        public void Start_Should_Call_GetAmountOfPlayersIntroMessage()
        {
            sut.Start(false);
            fake_IView.Verify(mock => mock.GetAmountOfPlayersIntroMessage());
        }

        [Fact]
        public void Start_Should_Call_GetPlayerNameIntroMessage()
        {
            sut.Start(false);
            fake_IView.Verify(mock => mock.GetPlayerNameIntroMessage());
        }

        [Fact]
        public void Start_Should_Call_GetAmountOfPlayers()
        {
            sut.Start(false);
            fake_IView.Verify(mock => mock.GetAmountOfPlayers(It.IsAny<string>()), Times.Once());
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
        public async void Play_InputPlayerListWith3Players_Should_Call_PlayerAction3TimesAsync()
        {
            await sut.Play(Fake3PlayerList(), fake_async_delay.Object, false);
            fake_IView.Verify(mock => mock.PlayerAction(It.IsAny<string>()), Times.Exactly(3));
        }

        [Fact]
        public async void Play_Should_Call_DisplayRolledDices()
        {
            await sut.Play(FakePlayerList(), fake_async_delay.Object, false);
            fake_IView.Verify(mock => mock.DisplayRolledDices(It.IsAny<string>(), It.IsAny<List<model.Dice>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Between(2, 8, Range.Inclusive));
        }

        [Fact]
        public async void Play_Should_Call_Roll1Time()
        {
            await sut.Play(FakePlayerList(), fake_async_delay.Object, false);
            fake_player.Verify(mock => mock.Roll(), Times.Once());
        }

        [Fact]
        public async void Play_Should_Call_UpdateScore1Time()
        {
            await sut.Play(FakePlayerList(), fake_async_delay.Object, false);
            fake_player.Verify(mock => mock.UpdateTotalScore(), Times.Once());
        }

        [Fact]
        public async void Play_Should_Call_DisplayRolledDices2Times()
        {
            await sut.Play(FakePlayerList(), fake_async_delay.Object, false);
            fake_IView.Verify(mock => mock.DisplayRolledDices(It.IsAny<string>(), It.IsAny<List<model.Dice>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionSave_Should_Call_PlayerGetHand3Times()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Save);
            fake_IView_local.Setup(mock => mock.GetDiceToSave(It.IsAny<string>())).Returns(model.Hand.Dices.Dice_1);

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_player.Verify(mock => mock.GetHand(), Times.Exactly(3));
        }

        [Fact]
        public async Task Play_PlayerActionReturnEnumActionQuit_Should_Throw_ValidateQuitException()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Quit);

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await Assert.ThrowsAsync<model.exception.ValidateQuitException>(() => sut_local.Play(FakePlayerList(), fake_async_delay.Object, false));
        }

        [Fact]
        public async Task Play_PlayerActionReturnEnumActionNewGame_Should_Throw_ValidateNewGameException()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.NewGame);

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await Assert.ThrowsAsync<model.exception.ValidateNewGameException>(() => sut_local.Play(FakePlayerList(), fake_async_delay.Object, false));
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionRoll_Should_Call_PlayerIsPlayerWinner1Time()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Roll);

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_player.Verify(mock => mock.IsPlayerWinner(), Times.Once());
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionRoll_Should_Call_DisplayWinner()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Roll);
            fake_IView_local.Setup(mock => mock.DisplayWinner(It.IsAny<string>(), It.IsAny<int>())).Verifiable();

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_IView_local.Verify(mock => mock.DisplayWinner(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void AddName_Should_Call_GetPlayername3Times()
        {
            List<model.IPlayer> playerList = sut.CreatePlayer(3);
            sut.AddName(playerList);
            fake_IView.Verify(mock => mock.GetPlayername(It.IsAny<string>()), Times.Exactly(3));
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionSave_Should_Call_GetDiceToSave5Times()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Save);
            fake_IView_local.Setup(mock => mock.GetDiceToSave(It.IsAny<string>())).Returns(model.Hand.Dices.Dice_1).Verifiable();

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_IView_local.Verify(mock => mock.GetDiceToSave(It.IsAny<string>()), Times.Exactly(6));
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionSave_Should_Call_DisplaySaveKeys6Times()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Save);
            fake_IView_local.Setup(mock => mock.GetDiceToSave(It.IsAny<string>())).Returns(model.Hand.Dices.Dice_1);
            fake_IView_local.Setup(mock => mock.DisplaySaveKeys()).Verifiable();

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_IView_local.Verify(mock => mock.DisplaySaveKeys(), Times.Exactly(6));
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionSave_Should_Call_Roll2Times()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Save);
            fake_IView_local.Setup(mock => mock.GetDiceToSave(It.IsAny<string>())).Returns(model.Hand.Dices.Dice_1);

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_player.Verify(mock => mock.Roll(), Times.Exactly(2));
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionSave_Should_Call_UpdateTotalScore()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Save);
            fake_IView_local.Setup(mock => mock.GetDiceToSave(It.IsAny<string>())).Returns(model.Hand.Dices.Dice_1);

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_player.Verify(mock => mock.UpdateTotalScore(), Times.Once());
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionSave_Should_Call_DisplayRolledDices2Times()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Save);
            fake_IView_local.Setup(mock => mock.GetDiceToSave(It.IsAny<string>())).Returns(model.Hand.Dices.Dice_1);
            fake_IView_local.Setup(mock => mock.DisplayRolledDices(It.IsAny<string>(), It.IsAny<List<model.Dice>>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_IView_local.Verify(mock => mock.DisplayRolledDices(It.IsAny<string>(), It.IsAny<List<model.Dice>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionSave_Should_Call_IsMoreDicesToRoll()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Save);
            fake_IView_local.Setup(mock => mock.GetDiceToSave(It.IsAny<string>())).Returns(model.Hand.Dices.Dice_1);

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_player.Verify(mock => mock.IsMoreDicesToRoll(), Times.Once());
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionSave_Should_Call_DisplayCannotSaveDiceTwice6Times()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Save);
            fake_IView_local.Setup(mock => mock.GetDiceToSave(It.IsAny<string>())).Returns(null);
            fake_IView_local.Setup(mock => mock.DisplayCannotSaveDiceTwice()).Verifiable();

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_IView_local.Verify(mock => mock.DisplayCannotSaveDiceTwice(), Times.Exactly(6));
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionSave_PlayerDontSaveAndClickDone_LoopBreak()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Save);
            fake_IView_local.Setup(mock => mock.GetDiceToSave(It.IsAny<string>())).Returns(model.Hand.Dices.Done);
            fake_IView_local.Setup(mock => mock.DisplayCannotSaveDiceTwice()).Verifiable();

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(Fake3PlayerList(), fake_async_delay.Object, false);

            fake_IView_local.Verify(mock => mock.DisplayCannotSaveDiceTwice(), Times.Exactly(0));
        }

        [Fact]
        public async void Play_PlayerActionReturnEnumActionQuit_Should_Call_EnvironmentExit()
        {
            var fake_IView_local = new Mock<view.IView>();
            fake_IView_local.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Quit);

            var sut_local = new controller.Farkle(fake_IView_local.Object, fake_envExit.Object);

            await sut_local.Play(FakePlayerList(), fake_async_delay.Object, false);

            fake_envExit.Verify(mock => mock.Exit(It.IsAny<int>()), Times.Once());
        }

        private void fake_IView_setup()
        {
            fake_IView.Setup(mock => mock.GetAmountOfPlayers(It.IsAny<string>())).Returns(3).Verifiable();
            fake_IView.Setup(mock => mock.DisplayRolledDices("Rogge", FakeDiceList(), 300, 2400)).Verifiable();
            fake_IView.Setup(mock => mock.PlayerAction(It.IsAny<string>())).Returns(view.FarkleView.Action.Roll).Verifiable();
            fake_IView.Setup(mock => mock.DisplayWinner(It.IsAny<string>(), It.IsAny<int>())).Verifiable();
            fake_IView.Setup(mock => mock.GetPlayername(It.IsAny<string>())).Returns("Sune").Verifiable();
            fake_IView.Setup(mock => mock.GetPlayerNameIntroMessage()).Verifiable();
            fake_IView.Setup(mock => mock.GetAmountOfPlayersIntroMessage()).Verifiable();
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
            fake_player.Setup(mock => mock.IsMoreDicesToRoll()).Returns(true).Verifiable();
        }

        private void fake_async_delay_setup()
        {
            fake_async_delay.Setup(mock => mock.Delay(It.IsAny<TimeSpan>())).Returns(Task.FromResult(0)).Verifiable();
        }

        private void fake_envExit_setup()
        {
            fake_envExit.Setup(mock => mock.Exit(It.IsAny<int>())).Verifiable();
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

        private List<IPlayer> Fake3PlayerList()
        {
            List<IPlayer> fakelist = new List<IPlayer>();
            fakelist.Add(fake_player.Object);
            fakelist.Add(fake_player.Object);
            fakelist.Add(fake_player.Object);
            return fakelist;
        }
    }
}
