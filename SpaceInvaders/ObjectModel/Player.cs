using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    public class Player : GameComponent
    {
        public string Nickname { get; set; }

        public int Score { get; set; }

        public int Lifes { get; set; }

        public Player(Game i_Game, string i_Nickname) : base(i_Game)
        {
            Lifes = int.Parse(ConfigurationManager.AppSettings["Player.InitAmountOfLifes"]);
            Score = 0;
            Nickname = i_Nickname;
        }

        public virtual void LoseLife()
        {
            int amountOfPointsToDecrease = int.Parse(ConfigurationManager.AppSettings["Scores.WhenLoseLife"]);
            Lifes--;
            Score = (Score - amountOfPointsToDecrease) < 0 ? 0 : (Score - amountOfPointsToDecrease);
        }

        public void AddScore(int i_Score)
        {
            Score += i_Score;
        }
    }
}
