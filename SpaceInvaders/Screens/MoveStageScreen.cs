using C15Ex02Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex02Dotan301810610Bar308000322.Screens
{
    public class MoveStageScreen : GameScreenWithTimer
    {
        private const int k_CountFromNumber=3;
        private int m_CurrentLevel = 0;
        private int m_CurrentCountingNum = 0;
        private Text m_CountingText;

        public MoveStageScreen(Game i_Game, int i_Level)
            : base(i_Game, TimeSpan.FromSeconds(k_CountFromNumber))
        {
            m_CurrentLevel = i_Level;
            m_CurrentCountingNum = k_CountFromNumber;
            Finished += MoveStageScreen_Finished;
        }

        public override void Initialize()
        {
            base.Initialize();

        }
        void MoveStageScreen_Finished(object sender, EventArgs e)
        {
            this.ScreensManager.Remove(this);
            this.ScreensManager.SetCurrentScreen(this.PreviousScreen);
        }
   
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.m_TimeLeft.TotalSeconds + 1 < m_CurrentCountingNum)
            {
                m_CurrentCountingNum--;
                m_CountingText.TextString = m_CurrentCountingNum.ToString();
            }

        }

        protected override void initTexts()
        {
            base.initTexts();
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            Text headText = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
            headText.Position = new Vector2(250, 200);
            headText.TintColor = Color.Lime;
            headText.TextString = "Starting level " + m_CurrentLevel;
            TextServices.CenterTextsOnScreen(this, new List<Text>() { headText });
            m_CountingText = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
            m_CountingText.Position = new Vector2(350, 300);
            m_CountingText.TintColor = Color.White;
            m_CountingText.TextString = m_CurrentCountingNum.ToString();
            TextServices.CenterTextsOnScreen(this, new List<Text>() { m_CountingText });
        }
    }
}
