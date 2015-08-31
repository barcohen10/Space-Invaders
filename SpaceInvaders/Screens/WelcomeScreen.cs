using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Menu.ConcreteMenuScreens;
using SpaceInvaders.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Services;

namespace SpaceInvaders.Screens
{
    public class WelcomeScreen : TitleScreen
    {
        public WelcomeScreen(Game i_Game)
            : base(i_Game, "Welcome To Space Invaders", Color.HotPink, "[Enter] - Start game", "[F6] - Main menu", "[Esc] - Exit game")
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.KeyPressed(Keys.Escape))
            {
                ScreensManager.Remove(this);
            }
            else if (InputManager.KeyPressed(Keys.Enter))
            {
                ScreensManager.SetCurrentScreen(new GamingScreen(this.Game));
                ScreensManager.SetCurrentScreen(new MoveStageScreen(this.Game, 1));
            }
            else if (InputManager.KeyPressed(Keys.F6))
            {
                ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
            }
        }

        public override void Initialize()
        {
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            base.Initialize();
        }
    }
}
