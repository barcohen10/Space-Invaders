using C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuScreens;
using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Screens
{
    public class GameOverScreen : TitleScreen
    {
        public GameOverScreen(Game i_Game)
            : base(i_Game, "Game Over", Color.Red, "[P] - Start new game", "[F6] - Main menu", "[Esc] - Exit game")
        {
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.Remove(this);
            }
            else if (InputManager.KeyPressed(Keys.Escape))
            {
               //ExitGame
            }
            else if (InputManager.KeyPressed(Keys.F6))
            {
                ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
            }
        }
    }
}
