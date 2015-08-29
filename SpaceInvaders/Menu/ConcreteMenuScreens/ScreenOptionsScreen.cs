using C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems;
using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuScreens
{
    public class ScreenOptionsScreen : MenuScreen
    {
        public ScreenOptionsScreen(Game i_Game)
            : base(i_Game, "Screen Options")
        {
        }
        protected override void InitMenuItems()
        {
            ToggleMenuItem mouseVisabilityItem = new ToggleMenuItem("Mouse Visability:", this, new List<string>() { "Visible", "Invisible" }, Keys.Enter,
                new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.TwoPlayers).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageDown },
                new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.OnePlayer).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageUp });
            ToggleMenuItem fullScreenItem = new ToggleMenuItem("Full Screen Mode:", this, new List<string>() { "On", "Off" }, Keys.Enter,
                new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.TwoPlayers).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageDown },
                new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.OnePlayer).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageUp });
            ToggleMenuItem allowResizingItem = new ToggleMenuItem("Allow Window Resizing:", this, new List<string>() { "On", "Off" }, Keys.Enter,
                new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.TwoPlayers).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageDown },
                new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.OnePlayer).RunMethod, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageUp });
            TextMenuItem doneItem = new TextMenuItem("Done", this, new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.Done).RunMethod, ActivateKey = Keys.Enter });
            AddMenuItems(mouseVisabilityItem, fullScreenItem, allowResizingItem, doneItem);
        }
    }
}
