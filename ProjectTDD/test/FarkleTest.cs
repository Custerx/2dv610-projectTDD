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
        private Mock<view.FarkleView> mock_farkleview;
        private controller.Farkle sut;

        public FarkleTest()
        {
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

        private void mock_farkleview_setup()
        {
            mock_farkleview.Setup(mock => mock.GetAmountOfPlayers(false)).Returns(3).Verifiable();
        }
    }
}
