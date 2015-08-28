using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Screens
{
    public abstract class TitleScreen : GameScreen
    {
        private string m_Title;
        private Color m_TitleColor;
        private List<string> m_Instructions;
        protected List<Text> m_InstructionsText;

        public TitleScreen(Game i_Game, string i_Title, Color i_TitleColor, params string[] i_Instructions)
            : base(i_Game)
        {
            m_Title = i_Title;
            m_TitleColor = i_TitleColor;
            m_Instructions = i_Instructions.ToList<string>();
        }

        public override void Initialize()
        {
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            base.Initialize();
        }

        protected override void initTexts()
        {
            Text titleText = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
            titleText.Position = new Vector2(0, 50);
            titleText.TintColor = m_TitleColor;
            titleText.TextString = m_Title;
            m_InstructionsText = TextServices.GetAndCreateTexts(this, m_Instructions, SpritesFactory.eSpriteType.MediumText, titleText.Position.X, titleText.Position.Y + 100f);
            TextServices.CenterTextsOnScreen(this, m_InstructionsText);
            TextServices.CenterTextsOnScreen(this, new List<Text>() { titleText });
        }

    }
}
