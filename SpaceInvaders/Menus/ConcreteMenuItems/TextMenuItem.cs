using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Services;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;
using SpaceInvaders.Infrastructure.ObjectModel.Menu;

namespace SpaceInvaders.Menus
{
    public class TextMenuItem : GameMenuItem
    {
        public TextMenuItem(string i_ItemName, GameScreen i_GameScreen, params MethodKey[] i_Methods)
            : base(i_ItemName, i_GameScreen, Color.White, GameMenuItem.eFontSize.Medium, i_Methods)
        {
        }
    }
}
