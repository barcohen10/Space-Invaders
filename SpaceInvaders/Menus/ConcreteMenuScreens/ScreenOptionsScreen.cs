using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Menus.ConcreteMenuItems;
using SpaceInvaders.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel.Menu;

namespace SpaceInvaders.Menus.ConcreteMenuScreens
{
    public class ScreenOptionsScreen : MenuScreen
    {
        private ScreenOptionsMng m_ScreenOptionsMng;

        public ScreenOptionsScreen(Game i_Game)
            : base(i_Game, "Screen Options")
        {
            m_ScreenOptionsMng = SpaceInvadersServices.GetScreenOptionsManager(this.Game);
        }

        protected override void InitMenuItems()
        {
            ToggleMenuItem mouseVisabilityItem = new ToggleMenuItem(
                "Mouse Visability:", this, m_ScreenOptionsMng.MouseVisibleStatus, new List<string>() { "Visible", "Invisible" }, Keys.Enter, new MethodKey() { MethodToRun = m_ScreenOptionsMng.MouseVisibilityOff, ActivateKey = Keys.PageDown }, new MethodKey() { MethodToRun = m_ScreenOptionsMng.MouseVisibilityOn, ActivateKey = Keys.PageUp });
            ToggleMenuItem fullScreenItem = new ToggleMenuItem(
                "Full Screen Mode:", this, m_ScreenOptionsMng.FullScreenMode, new List<string>() { "On", "Off" }, Keys.Enter, new MethodKey() { MethodToRun = m_ScreenOptionsMng.FullScreenOff, ActivateKey = Keys.PageDown }, new MethodKey() { MethodToRun = m_ScreenOptionsMng.FullScreenOn, ActivateKey = Keys.PageUp });
            ToggleMenuItem allowResizingItem = new ToggleMenuItem(
                "Allow Window Resizing:", this, m_ScreenOptionsMng.AllowWindowResizingMode, new List<string>() { "On", "Off" }, Keys.Enter, new MethodKey() { MethodToRun = m_ScreenOptionsMng.DisallowWindowResizing, ActivateKey = Keys.PageDown }, new MethodKey() { MethodToRun = m_ScreenOptionsMng.AllowWindowResizing, ActivateKey = Keys.PageUp });
            TextMenuItem doneItem = new TextMenuItem(
                "Done", this, new MethodKey() { MethodToRun = this.done, ActivateKey = Keys.Enter });
            AddMenuItems(mouseVisabilityItem, fullScreenItem, allowResizingItem, doneItem);
        }
    }
}
