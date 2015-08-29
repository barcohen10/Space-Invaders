using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems.RangeMenuItem
{
    public class Bar : Sprite
    {
        public Bar(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen) 
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
