using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class BarrierGroup : GameComponent
    {
        private const int k_NumOfBarries = 4;
        private List<Barrier> m_Barriers = new List<Barrier>();

        public BarrierGroup(Game i_Game)
            : base(i_Game)
        {
            createBarrierGroup(k_NumOfBarries);
        }

        private void createBarrierGroup(int i_NumOfBarriers)
        {
            Barrier barrier = null;
            Vector2 position = new Vector2(0, 0);
            float groupWidth = 0;

            for (int i = 0; i < i_NumOfBarriers; i++)
            {
                barrier = SpritesFactory.CreateSprite(this.Game, SpritesFactory.eSpriteType.Barrier) as Barrier;
                barrier.Position = position;
                position = new Vector2(position.X + (barrier.Width * 2), position.Y);
                barrier.TouchScreenLimit += changeMovingDirection;
                if (i > 0)
                {
                    barrier.Texture = new Microsoft.Xna.Framework.Graphics.Texture2D(barrier.GraphicsDevice, (int)barrier.Width, (int)barrier.Height);
                    barrier.Texture.SetData<Color>(m_Barriers[0].Pixels);
                    barrier.Pixels = m_Barriers[0].Pixels;
                }

                m_Barriers.Add(barrier);
            }

            groupWidth = barrier.Position.X + barrier.Width;
            float x = (float)this.Game.GraphicsDevice.Viewport.Width / 2;
            x -= groupWidth / 2;
            centerGroupOnScreen(new Vector2(x, 0));
        }

        private void centerGroupOnScreen(Vector2 i_Position)
        {
            m_Barriers[0].Position = new Vector2(i_Position.X, 0);
            for (int i = 1; i < k_NumOfBarries; i++)
            {
                m_Barriers[i].Position = new Vector2(m_Barriers[i - 1].Position.X + (m_Barriers[i - 1].Width * 2), 0);
            }
        }

        public void ChangeGroupPositionY(float i_PositionY)
        {
            for (int i = 0; i < k_NumOfBarries; i++)
            {
                m_Barriers[i].Position = new Vector2(m_Barriers[i].Position.X, i_PositionY);
            }
        }

        private void changeMovingDirection(object sender, EventArgs args)
        {
            foreach (Barrier barrier in m_Barriers)
            {
                barrier.Velocity = -barrier.Velocity;
            }
        }
    }
}
