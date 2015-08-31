using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Menu.ConcreteMenuItems;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Screens;
using SpaceInvaders.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel;

namespace SpaceInvaders.Menu.ConcreteMenuScreens
{
    public class MainMenuScreen : MenuScreen
    {
        private MultiPlayerConfiguration m_MultiPlayerConfiguration;

        public MainMenuScreen(Game i_Game)
            : base(i_Game, "Main Menu")
        {
            m_MultiPlayerConfiguration = SpaceInvadersServices.GetMultiPlayerConfiguration(this.Game);
            this.Game.IsMouseVisible = false;
        }

        protected override void InitMenuItems()
        {
            TextMenuItem screenOptionsItem = new TextMenuItem("Screen Options", this, new MethodKey() { MethodToRun = openScreenOptionsScreen, ActivateKey = Keys.Enter });
            ToggleMenuItem playersItem = new ToggleMenuItem(
                "Players:", this, m_MultiPlayerConfiguration.NumberOfPlayers.ToString(), new List<string>() { "One", "Two" }, Keys.Enter, new MethodKey() { MethodToRun = m_MultiPlayerConfiguration.ChangeToTwoPlayers, ActivateKey = Keys.PageDown }, new MethodKey() { MethodToRun = m_MultiPlayerConfiguration.ChangeToOnePlayer, ActivateKey = Keys.PageUp });
            TextMenuItem soundOptionsItem = new TextMenuItem("Sound Options", this, new MethodKey() { MethodToRun = openSoundOptionsScreen, ActivateKey = Keys.Enter });
            TextMenuItem playItem = new TextMenuItem("Play", this, new MethodKey() { MethodToRun = startPlay, ActivateKey = Keys.Enter });
            TextMenuItem quitItem = new TextMenuItem("Quit", this, new MethodKey() { MethodToRun = quitGame, ActivateKey = Keys.Enter });
            AddMenuItems(screenOptionsItem, playersItem, soundOptionsItem, playItem, quitItem);
            TextServices.CenterTextsOnScreen(this, null, new List<GameMenuItem>() { screenOptionsItem, playersItem, soundOptionsItem, playItem, quitItem });
        }

        private void startPlay()
        {
            ScreensManager.SetCurrentScreen(new GamingScreen(this.Game));
            ScreensManager.SetCurrentScreen(new MoveStageScreen(this.Game, 1));
        }

        private void openSoundOptionsScreen()
        {
            ScreensManager.SetCurrentScreen(new SoundOptionsScreen(this.Game));
        }

        private void openScreenOptionsScreen()
        {
            ScreensManager.SetCurrentScreen(new ScreenOptionsScreen(this.Game));
        }

        private void quitGame()
        {
            this.Game.Exit();
        }
    }
}
