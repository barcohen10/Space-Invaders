using C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems;
using C15Ex03Dotan301810610Bar308000322.Screens;
using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
            ToggleMenuItem playersItem = new ToggleMenuItem("Players:", this, new List<string>() { "One", "Two" }, Keys.Enter, new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.TwoPlayers).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageDown }, new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.OnePlayer).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageUp });
            TextMenuItem playItem = new TextMenuItem("Play", this, new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.Play).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.Enter });
            TextMenuItem quitItem = new TextMenuItem("Quit", this, new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.Quit).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.Enter });
            AddMenuItem(screenOptionsItem);
            AddMenuItem(playersItem);
            AddMenuItem(playItem);
            AddMenuItem(quitItem);
            TextServices.CenterTextsOnScreen(this, null, new List<GameMenuItem>() {screenOptionsItem, playersItem, playItem, quitItem});
        }

    }
}
