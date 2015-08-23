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
        private List<Text> m_TextInstructions;

        public WelcomeScreen(Game i_Game)
            : base(i_Game)
        {
            m_TextInstructions = new List<Text>();
        }

        public override void Initialize()
        {
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            m_WelcomeMessage = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.Text) as Text;
            m_WelcomeMessage.TextString = "Welcome To Space Invaders";
            m_WelcomeMessage.Scales = new Vector2(2f);
            base.Initialize();
        }

    }
}
