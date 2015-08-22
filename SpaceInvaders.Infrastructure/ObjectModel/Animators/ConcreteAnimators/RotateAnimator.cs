using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class RotateAnimator : SpriteAnimator
    {
        private float m_AngularVelocityDirection;
        private float k_AngularVelocity = (float)MathHelper.Pi;

        public RotateAnimator(int i_RoundsPerLength,eDirection i_Direction, TimeSpan i_AnimationLength)
            : base("RotateAnimator", i_AnimationLength)
        {
            m_AngularVelocityDirection = (int)i_Direction;
            k_AngularVelocity = (float)MathHelper.Pi * (float)i_RoundsPerLength;
        }


        public enum eDirection
        {
            Left = -1,
            Right = 1
        }
        public override void Initialize()
        {
            base.Initialize();
            this.BoundSprite.RotationOrigin = this.BoundSprite.SourceRectangleCenter;
        }

        protected override void RevertToOriginal()
        {
        }
        protected override void DoFrame(Microsoft.Xna.Framework.GameTime i_GameTime)
        {
            this.BoundSprite.AngularVelocity = k_AngularVelocity * m_AngularVelocityDirection;
        }

    }
}
