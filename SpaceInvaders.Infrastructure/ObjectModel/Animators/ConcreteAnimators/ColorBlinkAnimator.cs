using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class ColorBlinkAnimator : BlinkAnimator
    {
        private Color m_Color;

        // CTORs
        public ColorBlinkAnimator(string i_Name, Color i_Color, TimeSpan i_BlinkLength, TimeSpan i_AnimationLength)
            : base(i_Name, i_BlinkLength, i_AnimationLength)
        {
            m_Color = i_Color;
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.TintColor = m_OriginalSpriteInfo.TintColor;
        }

        protected override void DoWhenBlink()
        {
            if(!this.BoundSprite.TintColor.Equals(m_Color))
            {
                this.BoundSprite.TintColor = m_Color;
            }
            else
            {
                this.BoundSprite.TintColor = m_OriginalSpriteInfo.TintColor;
            }
        }

    }
}
