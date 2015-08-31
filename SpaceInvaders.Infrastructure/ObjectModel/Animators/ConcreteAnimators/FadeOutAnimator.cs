using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel.Animators;

namespace SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class FadeOutAnimator : SpriteAnimator
    {
        public FadeOutAnimator(TimeSpan i_AnimationLength)
            : base("FadeOutAnimator", i_AnimationLength)
        {
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;
        }

        protected override void DoFrame(Microsoft.Xna.Framework.GameTime i_GameTime)
        {
            if (this.BoundSprite.Opacity > 0)
            {
                this.BoundSprite.Opacity -= 0.01f;
            }
        }
    }
}
