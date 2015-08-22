using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModels;

namespace SpaceInvaders.Services
{
    public class CollisionServices
    {
        private static CollisionServices m_Instance;

        private CollisionServices()
        {
        }

        public enum eCollisionDirection
        {
            Top,
            Bottom
        }

        public static CollisionServices Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CollisionServices();
                }

                return m_Instance;
            }
        }

        public bool IsPixelsIntersect(Sprite i_Sprite, Sprite i_OtherSprite, out List<Vector2> o_CollidedPoints, bool i_IsAutoClear)
        {
            Rectangle SpriteRect = i_Sprite.Bounds, otherSpriteRect = i_OtherSprite.Bounds;
            Color[] SpritePixles = i_Sprite.Pixels, otherSpritePixles = i_OtherSprite.Pixels;
            bool isCollided = false;
            o_CollidedPoints = new List<Vector2>();
            int top = Math.Max(SpriteRect.Top, otherSpriteRect.Top);
            int bottom = Math.Min(SpriteRect.Bottom, otherSpriteRect.Bottom);
            int left = Math.Max(SpriteRect.Left, otherSpriteRect.Left);
            int right = Math.Min(SpriteRect.Right, otherSpriteRect.Right);
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorSprite = SpritePixles[(x - SpriteRect.Left) + ((y - SpriteRect.Top) * SpriteRect.Width)];
                    Color colorOtherSprite = otherSpritePixles[(x - otherSpriteRect.Left) + ((y - otherSpriteRect.Top) * otherSpriteRect.Width)];
                    if (colorSprite.A != 0 && colorOtherSprite.A != 0)
                    {
                        isCollided = true;
                        o_CollidedPoints.Add(new Vector2(x, y));
                        if (i_IsAutoClear)
                        {
                            SpritePixles[(x - SpriteRect.Left) + ((y - SpriteRect.Top) * SpriteRect.Width)].A = 0;
                        }
                    }
                }
            }

            if (i_IsAutoClear && isCollided)
            {
                i_Sprite.Pixels = SpritePixles;
            }

            return isCollided;
        }

        private bool isCollidedFromTop(Sprite i_Sprite, Sprite i_OtherSprite)
        {
            float spriteTop = i_Sprite.Position.Y, otherSpriteTop = i_OtherSprite.Position.Y;
            float spriteBottom = spriteTop + i_Sprite.Height;
            float otherSpriteBottom = otherSpriteTop + i_OtherSprite.Height;

            return otherSpriteTop < spriteTop && otherSpriteTop < spriteBottom;
        }

        public eCollisionDirection GetCollisionDirection(Sprite i_Sprite, Sprite i_OtherSprite)
        {
            eCollisionDirection collisionDirection = eCollisionDirection.Bottom;

            if (isCollidedFromTop(i_Sprite, i_OtherSprite))
            {
                collisionDirection = eCollisionDirection.Top;
            }

            return collisionDirection;
        }

        public void ClearPixelsInVerticalDirection(ref Sprite io_Sprite, List<Vector2> i_StartPixelPositions, eCollisionDirection i_Direction, int i_Length)
        {
            Color[] spritePixels = io_Sprite.Pixels;

            foreach (Vector2 position in i_StartPixelPositions)
            {
                int y = (int)position.Y, x = (int)position.X, index;
                switch (i_Direction)
                {
                    case eCollisionDirection.Top:
                        for (int i = 0; i < i_Length; i++, y++)
                        {
                            index = (x - io_Sprite.Bounds.Left) + ((y - io_Sprite.Bounds.Top) * io_Sprite.Bounds.Width);
                            if (index > -1 || !(index >= spritePixels.Length))
                            {
                                spritePixels[index].A = 0;
                            }
                            else
                            {
                                break;
                            }
                        }

                        break;

                    case eCollisionDirection.Bottom:
                        for (int i = 0; i < i_Length; i++, y--)
                        {
                            index = (x - io_Sprite.Bounds.Left) + ((y - io_Sprite.Bounds.Top) * io_Sprite.Bounds.Width);
                            if (index > -1 && !(index >= spritePixels.Length))
                            {
                                spritePixels[index].A = 0;
                            }
                            else
                            {
                                break;
                            }
                        }

                        break;
                }
            }

            io_Sprite.Pixels = spritePixels;
        }
    }
}
