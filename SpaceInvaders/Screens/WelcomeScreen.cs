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

        public override void Initialize()
        {
            initTexts();
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

           
        }
        private void initTexts()
        {
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            m_WelcomeMessage = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
            m_WelcomeMessage.Position = new Vector2(180,60);
            m_WelcomeMessage.TintColor = Color.HotPink;
            m_WelcomeMessage.TextString = "Welcome To Space Invaders";
            List<string> welcomeScreenTexts = new List<string>();
            welcomeScreenTexts.Add("[Enter] - Start game");
            welcomeScreenTexts.Add("[F6] - Main menu");
            welcomeScreenTexts.Add("[Esc] - Exit game");
            TextServices.CreateAndAdjustTexts(this, welcomeScreenTexts, SpritesFactory.eSpriteType.MediumText, m_WelcomeMessage.Position.X, m_WelcomeMessage.Position.Y + 50f);
        }


    }
}
