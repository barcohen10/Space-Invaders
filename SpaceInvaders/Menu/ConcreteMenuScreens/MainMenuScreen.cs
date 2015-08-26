using C15Ex03Dotan301810610Bar308000322.Screens;
using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuScreens
{
    public class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen(Game i_Game) : base(i_Game, "Main Menu")
        {
        }

        protected override void InitMenuItems()
        {
            TextMenuItem screenOptionsItem = new TextMenuItem("Screen Options", this);
            TextMenuItem playItem = new TextMenuItem("Play", this, new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.Play).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.Enter });
            TextMenuItem quitItem = new TextMenuItem("Quit", this, new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.Quit).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.Enter });
            AddMenuItem(screenOptionsItem);
            AddMenuItem(playItem);
            AddMenuItem(quitItem);
            TextServices.CenterTextsOnScreen(this, new List<Text>() { screenOptionsItem.Text, playItem.Text, quitItem.Text });
        }

    }
}
