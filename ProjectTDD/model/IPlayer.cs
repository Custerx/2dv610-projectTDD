using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDD.model
{
    public interface IPlayer
    {
        List<model.Dice> GetHand();
        List<model.Dice> GetSavedHand();
        void Play();
        void Roll();
        void Save(model.Dice a_dice);
        int GetTotalScore();
        int CalculateScore();
    }
}
