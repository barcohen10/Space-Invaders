using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class VisibleBlinkAnimator : BlinkAnimator
    {

        // CTORs
        public VisibleBlinkAnimator(string i_Name, TimeSpan i_BlinkLength, TimeSpan i_AnimationLength)
            : base(i_Name, i_BlinkLength, i_AnimationLength)
        {
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Visible = m_OriginalSpriteInfo.Visible;
        }

        protected override void DoWhenBlink()
        {
            this.BoundSprite.Visible = !this.BoundSprite.Visible;
        }

    }
}
