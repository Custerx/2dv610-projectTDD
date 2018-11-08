using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectTDD.test
{
    public class FarkleTest
    {
        private model.Farkle m_farkle;

        public FarkleTest()
        {
            m_farkle = new model.Farkle();
        }

        [Fact]
        public void Play_ListWith6Dices_return6()
        {
            List<model.Dice> diceList = m_farkle.Play();
            int actual = diceList.Count;
            int expected = 6;

            Assert.Equal(expected, actual);
        }
    }
}
