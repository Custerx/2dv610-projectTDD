using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ProjectTDD.test
{
    public class FarkleViewTest
    {
        private view.FarkleView sut;
        private readonly ITestOutputHelper output; // Capturing output.

        public FarkleViewTest()
        {
            sut = new view.FarkleView();
            output = new TestOutputHelper(); // https://xunit.github.io/docs/capturing-output
        }

        [Fact]
        public void DisplayDiceValues_ArgumentInt5_Output5()
        {
            model.Dice input = new model.Dice();
            sut.DisplayDice(input);
            // output.WriteLine(input);
        }

        [Fact]
        public void WantsToRollDice_NotPressingCharacterR_False()
        {
            bool success = sut.WantsToRollDice();
            Assert.False(success);
        }
    }
}
