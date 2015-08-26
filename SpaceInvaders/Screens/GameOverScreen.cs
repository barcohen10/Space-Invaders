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
    public class GameOverScreen : GameScreen
    {
        public GameOverScreen(Game i_Game)
            : base(i_Game)
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
        protected override void initTexts()
        {
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            Text topMessage = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
            topMessage.Position = new Vector2(250, 200);
            topMessage.TintColor = Color.HotPink;
            topMessage.TextString = "GameOver";
            List<string> welcomeScreenTexts = new List<string>();
            welcomeScreenTexts.Add("[P] - Start new game");
            welcomeScreenTexts.Add("[F6] - Main menu");
            welcomeScreenTexts.Add("[Esc] - Exit game");
            TextServices.GetAndCreateTexts(this, welcomeScreenTexts, SpritesFactory.eSpriteType.MediumText, topMessage.Position.X, topMessage.Position.Y + 50f);
        }
    }
}
