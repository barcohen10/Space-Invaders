using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Sound.ConcreteSounds
{
   public class BackgroundSound : Sound
    {
       public BackgroundSound(GameScreen i_GameScreen, string i_AssetName):base(i_GameScreen,i_AssetName)
        {
        }
       public BackgroundSound(Game i_Game, string i_AssetName)
           : base(i_Game, i_AssetName)
       {
       }
    }
}
