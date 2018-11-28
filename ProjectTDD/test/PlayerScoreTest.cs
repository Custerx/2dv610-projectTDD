using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectTDD.test
{
    public class PlayerScoreTest
    {
        [Fact]
        public void CalculateScore_Save555ThenRoll114_returnsIntScore700()
        {
            Mock<model.IHand> fake_hand_114555 = new Mock<model.IHand>();
            fake_hand_114555 = fake_hand_114555_setup(fake_hand_114555);
            model.IPlayer sutScore = new model.Player(fake_hand_114555.Object);

            int actual = sutScore.CalculateScore();
            int expected = (700); // Saved Dices (5 5 5) = Three 5's: 500 points. Rolled Dices (1 1 4), Two 1's: 100 * 2 = 200 points.
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateScore_Save555ThenRoll246_returnsIntScore0()
        {
            Mock<model.IHand> fake_hand_555246 = new Mock<model.IHand>();
            fake_hand_555246 = fake_hand_555246_setup(fake_hand_555246);
            model.IPlayer sutScore = new model.Player(fake_hand_555246.Object);

            int actual = sutScore.CalculateScore();
            int expected = 0; // Saved Dices (5 5 5) = Three 5's: 500 points. Rolled Dices (2 4 6) trigger Farkle.
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateScore_Roll333333_returnsIntScore3000()
        {
            Mock<model.IHand> fake_hand_333333 = new Mock<model.IHand>();
            fake_hand_333333 = fake_hand_333333_setup(fake_hand_333333);
            model.IPlayer sutScore = new model.Player(fake_hand_333333.Object);

            int actual = sutScore.CalculateScore();
            int expected = (3000); // Rolled Dices (3 3 3 3 3 3), Six 3's: 3000 points.
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateScore_Roll223466_returnsIntScore0()
        {
            Mock<model.IHand> fake_hand_223466 = new Mock<model.IHand>();
            fake_hand_223466 = fake_hand_223466_setup(fake_hand_223466);
            model.IPlayer sutScore = new model.Player(fake_hand_223466.Object);

            int actual = sutScore.CalculateScore();
            int expected = (0);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateScore_Roll123456_returnsIntScore3000()
        {
            Mock<model.IHand> fake_hand_123456 = new Mock<model.IHand>();
            fake_hand_123456 = fake_hand_123456_setup(fake_hand_123456);
            model.IPlayer sutScore = new model.Player(fake_hand_123456.Object);

            int actual = sutScore.CalculateScore();
            int expected = 3000;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateScore_Roll444422_returnsIntScore1500()
        {
            Mock<model.IHand> fake_hand_444422 = new Mock<model.IHand>();
            fake_hand_444422 = fake_hand_444422_setup(fake_hand_444422);
            model.IPlayer sutScore = new model.Player(fake_hand_444422.Object);

            int actual = sutScore.CalculateScore();
            int expected = 1500;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateScore_Roll334466_returnsIntScore1500()
        {
            Mock<model.IHand> fake_hand_334466 = new Mock<model.IHand>();
            fake_hand_334466 = fake_hand_334466_setup(fake_hand_334466);
            model.IPlayer sutScore = new model.Player(fake_hand_334466.Object);

            int actual = sutScore.CalculateScore();
            int expected = 1500;
            Assert.Equal(expected, actual);
        }

        private Mock<model.IHand> fake_hand_114555_setup(Mock<model.IHand> a_mockHand)
        {
            a_mockHand.Setup(mock => mock.Roll()).Verifiable();
            a_mockHand.Setup(mock => mock.Show()).Returns(fake_rolled3dice_list114());
            a_mockHand.Setup(mock => mock.ShowSaved()).Returns(fake_saved3dice_list555());
            return a_mockHand;
        }

        private List<model.Dice> fake_saved3dice_list555()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Five);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_4);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Five);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_5);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Five);
            fake_dice6.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_6);

            dicelist.Add(fake_dice4.Object);
            dicelist.Add(fake_dice5.Object);
            dicelist.Add(fake_dice6.Object);

            return dicelist;
        }

        private List<model.Dice> fake_rolled3dice_list114()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice1 = new Mock<model.Dice>();
            fake_dice1.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.One);
            fake_dice1.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);

            Mock<model.Dice> fake_dice2 = new Mock<model.Dice>();
            fake_dice2.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.One);
            fake_dice2.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            Mock<model.Dice> fake_dice3 = new Mock<model.Dice>();
            fake_dice3.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice3.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_3);

            dicelist.Add(fake_dice1.Object);
            dicelist.Add(fake_dice2.Object);
            dicelist.Add(fake_dice3.Object);

            return dicelist;
        }

        private Mock<model.IHand> fake_hand_333333_setup(Mock<model.IHand> a_mockHand)
        {
            a_mockHand.Setup(mock => mock.Roll()).Verifiable();
            a_mockHand.Setup(mock => mock.Show()).Returns(fake_rolled6dice_list333333());
            a_mockHand.Setup(mock => mock.ShowSaved()).Returns(fake_emptydice_list());
            return a_mockHand;
        }

        private List<model.Dice> fake_rolled6dice_list333333()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice1 = new Mock<model.Dice>();
            fake_dice1.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice1.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);

            Mock<model.Dice> fake_dice2 = new Mock<model.Dice>();
            fake_dice2.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice2.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            Mock<model.Dice> fake_dice3 = new Mock<model.Dice>();
            fake_dice3.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice3.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_3);

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_4);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_5);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice6.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_6);

            dicelist.Add(fake_dice1.Object);
            dicelist.Add(fake_dice2.Object);
            dicelist.Add(fake_dice3.Object);
            dicelist.Add(fake_dice4.Object);
            dicelist.Add(fake_dice5.Object);
            dicelist.Add(fake_dice6.Object);

            return dicelist;
        }

        private Mock<model.IHand> fake_hand_223466_setup(Mock<model.IHand> a_mockHand)
        {
            a_mockHand.Setup(mock => mock.Roll()).Verifiable();
            a_mockHand.Setup(mock => mock.Show()).Returns(fake_rolled6dice_list223466());
            a_mockHand.Setup(mock => mock.ShowSaved()).Returns(fake_emptydice_list());
            return a_mockHand;
        }

        private List<model.Dice> fake_rolled6dice_list223466()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice1 = new Mock<model.Dice>();
            fake_dice1.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Two);
            fake_dice1.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);

            Mock<model.Dice> fake_dice2 = new Mock<model.Dice>();
            fake_dice2.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Two);
            fake_dice2.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            Mock<model.Dice> fake_dice3 = new Mock<model.Dice>();
            fake_dice3.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice3.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_3);

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_4);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_5);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
            fake_dice6.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_6);

            dicelist.Add(fake_dice1.Object);
            dicelist.Add(fake_dice2.Object);
            dicelist.Add(fake_dice3.Object);
            dicelist.Add(fake_dice4.Object);
            dicelist.Add(fake_dice5.Object);
            dicelist.Add(fake_dice6.Object);

            return dicelist;
        }


        private Mock<model.IHand> fake_hand_123456_setup(Mock<model.IHand> a_mockHand)
        {
            a_mockHand.Setup(mock => mock.Roll()).Verifiable();
            a_mockHand.Setup(mock => mock.Show()).Returns(fake_rolled6dice_list123456());
            a_mockHand.Setup(mock => mock.ShowSaved()).Returns(fake_emptydice_list());
            return a_mockHand;
        }

        private List<model.Dice> fake_rolled6dice_list123456()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice1 = new Mock<model.Dice>();
            fake_dice1.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.One);
            fake_dice1.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);

            Mock<model.Dice> fake_dice2 = new Mock<model.Dice>();
            fake_dice2.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Two);
            fake_dice2.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            Mock<model.Dice> fake_dice3 = new Mock<model.Dice>();
            fake_dice3.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice3.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_3);

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_4);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Five);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_5);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
            fake_dice6.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_6);

            dicelist.Add(fake_dice1.Object);
            dicelist.Add(fake_dice2.Object);
            dicelist.Add(fake_dice3.Object);
            dicelist.Add(fake_dice4.Object);
            dicelist.Add(fake_dice5.Object);
            dicelist.Add(fake_dice6.Object);

            return dicelist;
        }

        private Mock<model.IHand> fake_hand_444422_setup(Mock<model.IHand> a_mockHand)
        {
            a_mockHand.Setup(mock => mock.Roll()).Verifiable();
            a_mockHand.Setup(mock => mock.Show()).Returns(fake_rolled6dice_list444422());
            a_mockHand.Setup(mock => mock.ShowSaved()).Returns(fake_emptydice_list());
            return a_mockHand;
        }

        private List<model.Dice> fake_rolled6dice_list444422()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice1 = new Mock<model.Dice>();
            fake_dice1.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice1.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);

            Mock<model.Dice> fake_dice2 = new Mock<model.Dice>();
            fake_dice2.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice2.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            Mock<model.Dice> fake_dice3 = new Mock<model.Dice>();
            fake_dice3.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice3.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_3);

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_4);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Two);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_5);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Two);
            fake_dice6.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_6);

            dicelist.Add(fake_dice1.Object);
            dicelist.Add(fake_dice2.Object);
            dicelist.Add(fake_dice3.Object);
            dicelist.Add(fake_dice4.Object);
            dicelist.Add(fake_dice5.Object);
            dicelist.Add(fake_dice6.Object);

            return dicelist;
        }

        private Mock<model.IHand> fake_hand_555246_setup(Mock<model.IHand> a_mockHand)
        {
            a_mockHand.Setup(mock => mock.Roll()).Verifiable();
            a_mockHand.Setup(mock => mock.Show()).Returns(fake_rolled3dice_list246());
            a_mockHand.Setup(mock => mock.ShowSaved()).Returns(fake_saved3dice_list666());
            return a_mockHand;
        }

        private List<model.Dice> fake_rolled3dice_list246()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Two);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
            fake_dice6.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_3);

            dicelist.Add(fake_dice4.Object);
            dicelist.Add(fake_dice5.Object);
            dicelist.Add(fake_dice6.Object);

            return dicelist;
        }

        private List<model.Dice> fake_saved3dice_list666()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_4);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_5);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
            fake_dice6.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_6);

            dicelist.Add(fake_dice4.Object);
            dicelist.Add(fake_dice5.Object);
            dicelist.Add(fake_dice6.Object);

            return dicelist;
        }

        private List<model.Dice> fake_emptydice_list()
        {
            List<model.Dice> emptyDiceList = new List<model.Dice>();
            return emptyDiceList;
        }

        private Mock<model.IHand> fake_hand_334466_setup(Mock<model.IHand> a_mockHand)
        {
            a_mockHand.Setup(mock => mock.Roll()).Verifiable();
            a_mockHand.Setup(mock => mock.Show()).Returns(fake_rolled6dice_list334466());
            a_mockHand.Setup(mock => mock.ShowSaved()).Returns(fake_emptydice_list());
            return a_mockHand;
        }

        private List<model.Dice> fake_rolled6dice_list334466()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice1 = new Mock<model.Dice>();
            fake_dice1.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice1.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);

            Mock<model.Dice> fake_dice2 = new Mock<model.Dice>();
            fake_dice2.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Three);
            fake_dice2.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            Mock<model.Dice> fake_dice3 = new Mock<model.Dice>();
            fake_dice3.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice3.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_3);

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_4);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_5);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
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
