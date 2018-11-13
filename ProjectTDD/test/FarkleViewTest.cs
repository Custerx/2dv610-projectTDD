﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectTDD.test
{
    public class FarkleViewTest
    {
        private view.FarkleView sut;

        public FarkleViewTest()
        {
            sut = new view.FarkleView();
        }

        [Fact]
        public void GetValue_BeforeDiceRoll_Return0()
        {
            int input = 5;
            sut.DisplayDiceValues(input); // How to check view output?
        }

        [Fact]
        public void WantsToPlay_DisplayView_True()
        {
            bool success = sut.WantsToPlay();
            Assert.True(success);
        }
    }
}
