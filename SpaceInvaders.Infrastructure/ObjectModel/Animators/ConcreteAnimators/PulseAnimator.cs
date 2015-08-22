using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class PulseAnimator : SpriteAnimator
    {
        protected float m_Scale;
        public float Scale
        {
            get { return m_Scale; }
            set { m_Scale = value; }
        }

        protected float m_PulsePerSecond;
        public float PulsePerSecond
        {
            get { return m_PulsePerSecond; }
            set { m_PulsePerSecond = value; }
        }

        private bool m_Shrinking;
        private float m_TargetScale;
        private float m_SourceScale;
        private float m_DeltaScale;

        public PulseAnimator(string i_Name, TimeSpan i_AnimationLength, float i_TargetScale, float i_PulsePerSecond)
            : base(i_Name, i_AnimationLength)
        {
            m_Scale = i_TargetScale;
            m_PulsePerSecond = i_PulsePerSecond;
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Scales = m_OriginalSpriteInfo.Scales;

            m_SourceScale = m_OriginalSpriteInfo.Scales.X;
            m_TargetScale = m_Scale;
            m_DeltaScale = m_TargetScale - m_SourceScale;
            m_Shrinking = m_DeltaScale < 0;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            float totalSeconds = (float)i_GameTime.ElapsedGameTime.TotalSeconds;

            if (m_Shrinking)
            {
                if (this.BoundSprite.Scales.X > m_TargetScale)
                {
                    this.BoundSprite.Scales -= new Vector2(totalSeconds * 2 * m_PulsePerSecond * m_DeltaScale);
                }
                else
                {
                    this.BoundSprite.Scales = new Vector2(m_TargetScale);
                    m_Shrinking = false;
                    m_TargetScale = m_SourceScale;
                    m_SourceScale = this.BoundSprite.Scales.X;
                }
            }
            else
            {
                if (this.BoundSprite.Scales.X < m_TargetScale)
                {
                    this.BoundSprite.Scales += new Vector2(totalSeconds * 2 * m_PulsePerSecond * m_DeltaScale);
                }
                else
                {
                    this.BoundSprite.Scales = new Vector2(m_TargetScale);
                    m_Shrinking = true;
                    m_TargetScale = m_SourceScale;
                    m_SourceScale = this.BoundSprite.Scales.X;
                }
            }
        }
    }
}
