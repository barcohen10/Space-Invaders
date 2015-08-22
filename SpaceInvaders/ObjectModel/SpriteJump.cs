using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModels;

namespace SpaceInvaders.ObjectModel
{
    public class SpriteJump
    {
        private Sprite m_Sprite;

        private SpriteOverJumped m_SpriteOverJumpedData = new SpriteOverJumped();

        public enum eJumpDirection
        {
            Left,
            Right,
            DownAndRight,
            DownAndLeft
        }

        public struct SpriteOverJumped
        {
            public float DistanceToJumpBackwards { get; set; }

            public eJumpDirection DirectionToJumpBackwards { get; set; }

            public bool IsTouchedScreenHorizontalBoundary { get; set; }
        }

        public SpriteJump(Sprite i_Sprite)
        {
            m_Sprite = i_Sprite;
        }

        public void Jump(eJumpDirection i_JumpDirection, float i_DistanceToJump, bool i_IsJumpingBackwards)
        {
            switch (i_JumpDirection)
            {
                case eJumpDirection.Right:

                    m_Sprite.Position = new Vector2(m_Sprite.Position.X + i_DistanceToJump, m_Sprite.Position.Y);
                    if (!i_IsJumpingBackwards)
                    {
                        m_SpriteOverJumpedData.IsTouchedScreenHorizontalBoundary = ((m_Sprite.Position.X + m_Sprite.Width) >= m_Sprite.Game.GraphicsDevice.Viewport.Width) ? true : false;
                        if (m_SpriteOverJumpedData.IsTouchedScreenHorizontalBoundary)
                        {
                            m_SpriteOverJumpedData.DirectionToJumpBackwards = eJumpDirection.Left;
                            m_SpriteOverJumpedData.DistanceToJumpBackwards = (m_Sprite.Position.X + m_Sprite.Width) - m_Sprite.Game.GraphicsDevice.Viewport.Width;
                        }
                    }

                    break;

                case eJumpDirection.Left:
                    m_Sprite.Position = new Vector2(m_Sprite.Position.X - i_DistanceToJump, m_Sprite.Position.Y);
                    if (!i_IsJumpingBackwards)
                    {
                        m_SpriteOverJumpedData.IsTouchedScreenHorizontalBoundary = (m_Sprite.Position.X <= 0) ? true : false;
                        if (m_SpriteOverJumpedData.IsTouchedScreenHorizontalBoundary)
                        {
                            m_SpriteOverJumpedData.DirectionToJumpBackwards = eJumpDirection.Right;
                            m_SpriteOverJumpedData.DistanceToJumpBackwards = Math.Abs(m_Sprite.Position.X);
                        }
                    }

                    break;

                case eJumpDirection.DownAndRight:
                case eJumpDirection.DownAndLeft:
                    if (m_Sprite.Position.Y + i_DistanceToJump < m_Sprite.Game.GraphicsDevice.Viewport.Height)
                    {
                        m_Sprite.Position = new Vector2(m_Sprite.Position.X, m_Sprite.Position.Y + i_DistanceToJump);
                    }
                    else
                        m_Sprite.Position = new Vector2(m_Sprite.Position.X, m_Sprite.Game.GraphicsDevice.Viewport.Height - m_Sprite.Height);
                    m_SpriteOverJumpedData.IsTouchedScreenHorizontalBoundary = false;
                    break;
            }
        }

        public SpriteOverJumped OverJumpedData
        {
            get
            {
                return m_SpriteOverJumpedData;
            }

            set
            {
                m_SpriteOverJumpedData = value;
            }
        }

        public bool IsTouchedEndOfTheScreen()
        {
            bool isTouchedEndOfTheScreen = false;

            if (m_Sprite.Position.Y + m_Sprite.Height >= m_Sprite.Game.GraphicsDevice.Viewport.Height)
            {
                isTouchedEndOfTheScreen = true;
            }

            return isTouchedEndOfTheScreen;
        }
    }
}
