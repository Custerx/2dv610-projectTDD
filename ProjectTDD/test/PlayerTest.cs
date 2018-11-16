using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectTDD.test
{
    public class PlayerTest
    {
        private model.Player sut;
        private model.Player sutRealHand;
        private Mock<model.Hand> fake_hand;
        private model.Hand real_hand;

        public PlayerTest()
        {
            real_hand = new model.Hand();
            sutRealHand = new model.Player(real_hand);

            fake_hand = new Mock<model.Hand>();
            fake_hand_setup();
            sut = new model.Player(fake_hand.Object);
        }

        [Fact]
        public void GetHand_Show_ReturnFake6DiceList()
        {
            int actual = sut.GetHand().Count;
            int expected = 6;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetHand_CallPlayAnd_Return6DiceList()
        {
            sutRealHand.Play();
            int actual = sutRealHand.GetHand().Count;
            int expected = 6;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Roll_CallRollAnd_Verify()
        {
            sut.Roll();
            fake_hand.Verify(mock => mock.Roll(), Times.Once());
        }

        [Fact]
        public void GetSavedHand_ShowSaved_ReturnFake6SavedDiceList()
        {
            int actual = sut.GetSavedHand().Count;
            int expected = 6;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSavedHand_CallPlayAnd_Return6SavedDiceList()
        {
            sutRealHand.Play();
            List<model.Dice> diceList = sutRealHand.GetHand();
            List<model.Dice> toBeSaved = new List<model.Dice>();

            // IMPORTANT! Done to avoid index issues.
            foreach (model.Dice d in diceList)
            {
                toBeSaved.Add(d);
            }

            for (int i=0; i < toBeSaved.Count; i++)
            {
                sutRealHand.Save(toBeSaved[i]);
            }

            int actual = sutRealHand.GetSavedHand().Count;
            int expected = 6;
            Assert.Equal(expected, actual);

            // Confirm roll list is empty.
            int notSavedActual = sutRealHand.GetHand().Count;
            int notSavedExpected = 0;
            Assert.Equal(notSavedExpected, notSavedActual);
        }

        private void fake_hand_setup()
        {
            fake_hand.Setup(mock => mock.Roll()).Verifiable();
            fake_hand.Setup(mock => mock.Show()).Returns(fake_6dice_list());
            fake_hand.Setup(mock => mock.ShowSaved()).Returns(fake_6dice_list());
        }

        private List<model.Dice> fake_6dice_list()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice1 = new Mock<model.Dice>();
            fake_dice1.Setup(mock => mock.GetValue()).Returns(5);
            fake_dice1.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);

            Mock<model.Dice> fake_dice2 = new Mock<model.Dice>();
            fake_dice2.Setup(mock => mock.GetValue()).Returns(1);
            fake_dice2.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            Mock<model.Dice> fake_dice3 = new Mock<model.Dice>();
            fake_dice3.Setup(mock => mock.GetValue()).Returns(1);
            fake_dice3.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_3);

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(6);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_4);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(4);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_5);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(1);
            fake_dice6.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_6);

            dicelist.Add(fake_dice1.Object);
            dicelist.Add(fake_dice2.Object);
            dicelist.Add(fake_dice3.Object);
            dicelist.Add(fake_dice4.Object);
            dicelist.Add(fake_dice5.Object);
            dicelist.Add(fake_dice6.Object);

            return dicelist;
        }
    }
}
