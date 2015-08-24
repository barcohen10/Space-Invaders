﻿using Microsoft.Xna.Framework;
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
        private int m_CurrentLevel = 0;
        private int m_CurrentCountingNum = 0;
        private Text m_CountingText;
        public MoveStageScreen(Game i_Game, int i_Level)
            : base(i_Game, TimeSpan.FromSeconds(3))
        {
            m_CurrentLevel = i_Level;
            Finished += MoveStageScreen_Finished;
            m_CurrentCountingNum = 3;
        }
        public override void Initialize()
        {
            base.Initialize();

        }
        void MoveStageScreen_Finished(object sender, EventArgs e)
        {
            this.ExitScreen();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.m_TimeLeft.TotalSeconds < m_CurrentCountingNum)
            {
                m_CurrentCountingNum--;
                m_CountingText.TextString = m_CurrentCountingNum.ToString();
            }

        }
        protected override void initTexts()
        {
            base.initTexts();
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            Text headText = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.MediumText) as Text;
            headText.Position = new Vector2(250, 200);
            headText.TintColor = Color.Lime;
            headText.TextString = "Moving to level : " + m_CurrentLevel;
            m_CountingText = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
            m_CountingText.Position = new Vector2(350, 250);
            m_CountingText.TintColor = Color.White;
            m_CountingText.TextString = m_CurrentCountingNum.ToString();
        }
    }
}