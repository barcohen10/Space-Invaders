using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public abstract class BlinkAnimator : SpriteAnimator
    {
        private TimeSpan m_BlinkLength;
        private TimeSpan m_TimeLeftForNextBlink;

        public TimeSpan BlinkLength
        {
            get { return m_BlinkLength; }
            set { m_BlinkLength = value; }
        }

        // CTORs
        public BlinkAnimator(string i_Name, TimeSpan i_BlinkLength, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            this.m_BlinkLength = i_BlinkLength;
            this.m_TimeLeftForNextBlink = i_BlinkLength;
        }

        public BlinkAnimator(TimeSpan i_BlinkLength, TimeSpan i_AnimationLength)
            : this("Blink", i_BlinkLength, i_AnimationLength)
        {
            this.m_BlinkLength = i_BlinkLength;
            this.m_TimeLeftForNextBlink = i_BlinkLength;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            m_TimeLeftForNextBlink -= i_GameTime.ElapsedGameTime;
            if (m_TimeLeftForNextBlink.TotalSeconds < 0)
            {
                // we have elapsed, so blink
                DoWhenBlink();
                m_TimeLeftForNextBlink = m_BlinkLength;
            }
        }

        protected abstract void DoWhenBlink();
    }
}
