using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Xunit.Sdk;

namespace ProjectTDD.test
{
    public class FarkleTest
    {
        private Mock<model.Dice> fake_dice;
        private Mock<model.Hand> mock_hand;
        private Mock<view.FarkleView> mock_farkleview;
        private controller.Farkle sut;

        public FarkleTest()
        {
            fake_dice = new Mock<model.Dice>();
            mock_hand = new Mock<model.Hand>();
            mock_farkleview = new Mock<view.FarkleView>();
            fake_dice_setup();
            mock_hand_setup();
            mock_farkleview_setup();
            sut = new controller.Farkle(mock_hand.Object, mock_farkleview.Object);
        }

        [Fact]
        public void Start_Should_Call_Play()
        {
            sut.Start();
            mock_hand.Verify(mock => mock.Play(), Times.Once());
        }

        [Fact]
        public void Start_Should_Call_DisplayRolledDices()
        {
            sut.Start();
            mock_farkleview.Verify(mock => mock.DisplayRolledDices(It.IsAny<string>(), It.IsAny<List<model.Dice>>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void Start_Should_Call_WantsToRollDice()
        {
            sut.Start();
            mock_farkleview.Verify(mock => mock.WantsToRollDice(), Times.Once());
        }

        private void fake_dice_setup()
        {
            fake_dice.Setup(mock => mock.GetValue()).Returns(5);
            fake_dice.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);
        }

        private void mock_hand_setup()
        {
            mock_hand.Setup(mock => mock.Play()).Verifiable();
            mock_hand.Setup(mock => mock.Show()).Returns(fake_dice_list());
        }

        private void mock_farkleview_setup()
        {
            string fakeName = "Rogge";
            int fakeScore = 300;

            mock_farkleview.Setup(mock => mock.DisplayRolledDices(fakeName, fake_dice_list(), fakeScore)).Verifiable();
            mock_farkleview.Setup(mock => mock.WantsToRollDice()).Verifiable();
        }

        private List<model.Dice> fake_dice_list()
        {
            List<model.Dice> dicelist = new List<model.Dice>();
            dicelist.Add(fake_dice.Object);
            return dicelist;
        }
    }
}
