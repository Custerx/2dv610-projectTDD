﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    public interface IPlayer
    {
        List<model.Dice> GetHand();
        List<model.Dice> GetSavedHand();
        void Roll();
        void Save(model.Dice a_dice);
        int GetTotalScore();
        int CalculateScore();
        bool IsPlayerWinner();
        bool IsFarkle(int a_savedDiceScore, int a_rolledDiceScore);
        void UpdateTotalScore();
        void SetPlayername(string a_name);
        string GetPlayername();
        bool IsMoreDicesToRoll();
    }
}
