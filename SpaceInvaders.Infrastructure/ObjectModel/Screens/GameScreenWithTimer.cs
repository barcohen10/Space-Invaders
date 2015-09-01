﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Infrastructure.ObjectModel.Screens
{
    public class GameScreenWithTimer : GameScreen
    {
        protected TimeSpan m_ScreenTime;
        protected TimeSpan m_TimeLeft;

        public GameScreenWithTimer(Game i_Game, TimeSpan i_ScreenVisibleTime)
            : base(i_Game)
        {
            m_ScreenTime = i_ScreenVisibleTime;
            m_TimeLeft = m_ScreenTime;
        }

        public event EventHandler Finished;

        protected virtual void OnFinished()
        {
            if (Finished != null)
            {
                Finished(this, EventArgs.Empty);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_TimeLeft -= gameTime.ElapsedGameTime;

            if (m_TimeLeft.TotalSeconds < 0)
            {
                m_TimeLeft = m_ScreenTime;
                OnFinished();
            }
        }
    }
}
