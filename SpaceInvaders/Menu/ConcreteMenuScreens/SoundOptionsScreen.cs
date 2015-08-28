﻿using C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems;
using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuScreens
{
    public class SoundOptionsScreen : MenuScreen
    {
        public SoundOptionsScreen(Game i_Game)
            : base(i_Game, "Sound Options")
        {
        }
        protected override void InitMenuItems()
        {
            ToggleMenuItem toggleSoundItem = new ToggleMenuItem("Toggle Sound:", this, new List<string>() { "On", "Off" }, Keys.Enter,
                new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.TwoPlayers).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageDown },
                new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.OnePlayer).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageUp });
            ////2 more range menu items////
            TextMenuItem doneItem = new TextMenuItem("Done", this, new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.Done).RunMethod, ActivateKey = Keys.Enter });
            AddMenuItems(toggleSoundItem, doneItem);
           // TextServices.CenterTextsOnScreen(this, null, new List<GameMenuItem>() { toggleSoundItem, doneItem});
        }
    }
}