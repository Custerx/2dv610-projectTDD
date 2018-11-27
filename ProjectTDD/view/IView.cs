﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.view
{
    public interface IView
    {
        void DisplayGameKeys();
        void DisplayDice(model.Dice a_dice);
        void DisplayRolledDices(String a_player, List<model.Dice> a_hand, int a_score, int a_totalScore);
        bool GetAction(model.IPlayer player, string a_letter = null, bool a_test = false);
        int GetAmountOfPlayers(bool a_isThisATest = false);
        view.FarkleView.Action PlayerAction(string a_input = null);
        void DisplayWinner(String a_player, int a_totalScore);
        string GetPlayername(string a_input = null);
        model.Hand.Dices GetDiceToSave(bool a_isThisATest = false);
        void DisplaySaveKeys();
        void DisplayCannotSaveDiceTwice();
    }
}
