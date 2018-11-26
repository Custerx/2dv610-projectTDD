﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    public interface IHand
    {
        bool NoMoreThan6DicesInPlay();
        void Roll();
        List<model.Dice> Show();
        List<model.Dice> ShowSaved();
        bool Save(model.Dice a_dice);
        void Reset();
    }
}
