using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace ProjectTDD.test
{
    public class FarkleTest
    {
        private Mock<model.Hand> mock_hand;
        private Mock<view.FarkleView> mock_farkleview;
        private controller.Farkle sut;

        public FarkleTest()
        {
            mock_hand = new Mock<model.Hand>();
            mock_farkleview = new Mock<view.FarkleView>();
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

        private void mock_hand_setup()
        {
            mock_hand.Setup(mock => mock.Play()).Verifiable();
        }

        private void mock_farkleview_setup()
        {
            mock_farkleview.Setup(mock => mock.DisplayDiceValues(5));
        }
    }
}
