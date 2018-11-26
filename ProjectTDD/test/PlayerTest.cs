using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectTDD.test
{
    public class PlayerTest
    {
        private model.IPlayer sut;
        private model.IPlayer sutRealHand;
        private Mock<model.IHand> fake_hand;
        private model.IHand real_hand;

        public PlayerTest()
        {
            real_hand = new model.Hand();
            sutRealHand = new model.Player(real_hand);

            fake_hand = new Mock<model.IHand>();
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
        public void GetSavedHand_ShowSaved_ReturnFakeEmptySavedDiceList()
        {
            int actual = sut.GetSavedHand().Count;
            int expected = 0;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSavedHand_CallPlayAnd_Return6SavedDiceList()
        {
            List<model.Dice> diceList = sutRealHand.GetHand();
            List<model.Dice> toBeSaved = new List<model.Dice>();

            // IMPORTANT! Done to avoid index issues.
            foreach (model.Dice d in diceList)
            {
                toBeSaved.Add(d);
            }

            for (int i = 0; i < toBeSaved.Count; i++)
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

        [Fact]
        public void Save_FakeDice_ThrowsDiceNotFoundException()
        {
            Mock<model.Dice> fake_dice1 = new Mock<model.Dice>();
            fake_dice1.Setup(mock => mock.GetValue()).Returns(null);
            fake_dice1.Setup(mock => mock.Dicenumber).Returns(null);

            Assert.Throws<model.exception.DiceNotFoundException>(() => sutRealHand.Save(fake_dice1.Object));
        }

        [Fact]
        public void CalculateScore_Roll111456_returnsIntScore1050()
        {
            int actual = sut.CalculateScore();
            int expected = 1000 + 50; // Three 1's: 1000 points. 5: 50 points.
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotalScore_RunCalculateScore_returnsIntScore1050()
        {
            sut.UpdateTotalScore();
            int actual = sut.GetTotalScore();
            int expected = 1000 + 50; // Three 1's: 1000 points. 5: 50 points.
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotalScore_RunCalculateScoreTwice_returnsIntScore2100()
        {
            sut.UpdateTotalScore();
            sut.UpdateTotalScore();
            int actual = sut.GetTotalScore();
            int expected = (1000 + 50) * 2; // Three 1's: 1000 points. 5: 50 points.
            Assert.Equal(expected, actual);
        }

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
        public void IsFarkle_SavedScoreAbove0RolledScore0_true()
        {
            int inputSavedScore = 100;
            int inputRolledScore = 0;
            bool success = sut.IsFarkle(inputSavedScore, inputRolledScore);

            Assert.True(success);
        }

        [Fact]
        public void IsFarkle_SavedScore0RolledScore0_false()
        {
            int inputSavedScore = 0;
            int inputRolledScore = 0;
            bool actual = sut.IsFarkle(inputSavedScore, inputRolledScore);

            Assert.False(actual);
        }

        [Fact]
        public void IsPlayerWinner_TotalScoreOf0Points_false()
        {
            bool no = sut.IsPlayerWinner();
            Assert.False(no);
        }

        [Fact]
        public void IsPlayerWinner_TotalScoreOf10500Points_true()
        {
            for (int i = 0; i < 10; i++) // Total: 10500 points.
            {
                sut.UpdateTotalScore(); // 1050 points.
            }

            bool yes = sut.IsPlayerWinner();
            Assert.True(yes);
        }

        [Fact]
        public void GetPlayername_SetPlayernameRogge_stringRogge()
        {
            sut.SetPlayername("Rogge");
            string actual = sut.GetPlayername();
            string expected = "Rogge";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetPlayername_Emptystring_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.SetPlayername(""));
        }

        [Fact]
        public void UpdateTotalScore_Should_Call_Reset()
        {
            sut.UpdateTotalScore();
            fake_hand.Verify(mock => mock.Reset(), Times.Once());
        }

        private void fake_hand_setup()
        {
            fake_hand.Setup(mock => mock.Roll()).Verifiable();
            fake_hand.Setup(mock => mock.Show()).Returns(fake_6dice_list());
            fake_hand.Setup(mock => mock.ShowSaved()).Returns(fake_emptydice_list());
            fake_hand.Setup(mock => mock.Reset()).Verifiable();
        }

        private List<model.Dice> fake_6dice_list()
        {
            List<model.Dice> dicelist = new List<model.Dice>();

            Mock<model.Dice> fake_dice1 = new Mock<model.Dice>();
            fake_dice1.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Five);
            fake_dice1.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_1);

            Mock<model.Dice> fake_dice2 = new Mock<model.Dice>();
            fake_dice2.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.One);
            fake_dice2.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_2);

            Mock<model.Dice> fake_dice3 = new Mock<model.Dice>();
            fake_dice3.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.One);
            fake_dice3.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_3);

            Mock<model.Dice> fake_dice4 = new Mock<model.Dice>();
            fake_dice4.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Six);
            fake_dice4.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_4);

            Mock<model.Dice> fake_dice5 = new Mock<model.Dice>();
            fake_dice5.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.Four);
            fake_dice5.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_5);

            Mock<model.Dice> fake_dice6 = new Mock<model.Dice>();
            fake_dice6.Setup(mock => mock.GetValue()).Returns(model.Dice.DiceValue.One);
            fake_dice6.Setup(mock => mock.Dicenumber).Returns(model.Hand.Dices.Dice_6);

            dicelist.Add(fake_dice1.Object);
            dicelist.Add(fake_dice2.Object);
            dicelist.Add(fake_dice3.Object);
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
    }
}
