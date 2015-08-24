using C15Ex02Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex02Dotan301810610Bar308000322.Screens
{
    public class WelcomeScreen : GameScreen
    {
        private Text m_WelcomeMessage;

        public WelcomeScreen(Game i_Game)
            : base(i_Game)
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
            }
            else if (InputManager.KeyPressed(Keys.F6))
            {
                ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
            }

        }

        protected override void initTexts()
        {
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            m_WelcomeMessage = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
            m_WelcomeMessage.Position = new Vector2(0, 50);
            m_WelcomeMessage.TintColor = Color.HotPink;
            m_WelcomeMessage.TextString = "Welcome To Space Invaders";
            List<string> menuItems = new List<string>();
            menuItems.Add("[Enter] - Start game");
            menuItems.Add("[F6] - Main menu");
            menuItems.Add("[Esc] - Exit game");
            List<Text> listMenuTexts = TextServices.GetAndCreateTexts(this, menuItems, SpritesFactory.eSpriteType.MediumText, m_WelcomeMessage.Position.X, m_WelcomeMessage.Position.Y + 100f);
            TextServices.CenterTextsOnScreen(this, listMenuTexts);
            TextServices.CenterTextsOnScreen(this, new List<Text>() { m_WelcomeMessage});
        }


    }
}
