﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.view
{
    public interface IView
    {
        void DisplayGameKeys();
        void DisplayDice(model.Dice a_dice);
        void DisplayRolledDices(String a_player, List<model.Dice> a_hand, int a_score);
        bool GetAction(model.IPlayer player, string a_letter = null, bool a_test = false);
        int GetAmountOfPlayers(bool a_isThisATest = false);

    }
}
