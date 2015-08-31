using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class ShrinkAnimator : SpriteAnimator
    {
        public ShrinkAnimator(string i_Name, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
        }

        public ShrinkAnimator(TimeSpan i_AnimationLength)
            : this("ShrinkAnimator", i_AnimationLength)
        {
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Scales = m_OriginalSpriteInfo.Scales;
        }

        protected override void DoFrame(Microsoft.Xna.Framework.GameTime i_GameTime)
        {
            BoundSprite.Scales -= new Vector2(0.3f * (float)i_GameTime.ElapsedGameTime.TotalSeconds);
            float opacityDelta = 0.5f * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            BoundSprite.Opacity = Math.Max(0, this.BoundSprite.Opacity - opacityDelta);
            if (BoundSprite.Width <= 0 || BoundSprite.Width <= 0)
            {
                this.IsFinished = true;
            }
        }
    }
}
