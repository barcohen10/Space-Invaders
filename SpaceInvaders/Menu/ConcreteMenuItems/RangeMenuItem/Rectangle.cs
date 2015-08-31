using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;

namespace SpaceInvaders.Menu.ConcreteMenuItems.RangeMenuItem
{
    public class Rectangle : Sprite
    {
        public Rectangle(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen) 
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
