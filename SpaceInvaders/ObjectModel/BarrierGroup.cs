using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Services;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.ObjectModel
{
    public class BarrierGroup : GameComponent
    {
        private const int k_NumOfBarries = 4;
        private List<Barrier> m_Barriers;
        private GameScreen m_GameScreen;
        private float m_CurrentBarriersSpeed;
        private static Color[] m_OriginalPixels;
        private static bool m_FirstRun= true;

        public BarrierGroup(GameScreen i_GameScreen)
            : base(i_GameScreen.Game)
        {
            m_GameScreen = i_GameScreen;
            m_Barriers = new List<Barrier>();
        }

        public void ChangeToDefaultJumpingSpeed()
        {
            foreach (Barrier barrier in m_Barriers)
            {
                barrier.ChangeToDefaultJumpingSpeed();
            }

            m_CurrentBarriersSpeed = m_Barriers[0].CurrentSpeed;
        }

        public bool compare(Color[] i_first, Color[] i_second)
        {
            bool same = true;
            for (int i = 0; i < i_first.Length; i++)
            {
                if (i_first[i] != i_second[i])
                {
                    same = false;
                }
            }

            return same;
        }

        private void createBarrierGroup(int i_NumOfBarriers)
        {
            Barrier barrier = null;
            Vector2 position = new Vector2(0, 0);
            float groupWidth = 0;
            m_Barriers.Clear();
            for (int i = 0; i < i_NumOfBarriers; i++)
            {
                barrier = SpritesFactory.CreateSprite(m_GameScreen, SpritesFactory.eSpriteType.Barrier) as Barrier;
                if (m_FirstRun)
                {
                    m_OriginalPixels = barrier.Pixels.Clone() as Color[];
                    m_FirstRun = false;
                }
                else if (i == 0)
                {
                    barrier.Pixels = m_OriginalPixels.Clone() as Color[];
                }

                barrier.Position = position;
                position = new Vector2(position.X + (barrier.Width * 2), position.Y);
                barrier.TouchScreenLimit += changeMovingDirection;
                if (i > 0)
                {
                    barrier.Texture = new Microsoft.Xna.Framework.Graphics.Texture2D(barrier.GraphicsDevice, (int)barrier.Width, (int)barrier.Height);
                    barrier.Pixels = m_OriginalPixels.Clone() as Color[];
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

        public override void Initialize()
        {
            base.Initialize();
            createBarrierGroup(k_NumOfBarries);
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

        public void Speedup(float i_Percenteage)
        {
            m_CurrentBarriersSpeed += m_CurrentBarriersSpeed * i_Percenteage;
            foreach (Barrier barrier in m_Barriers)
            {
                barrier.SpeedUp((float)m_CurrentBarriersSpeed);
            }
        }

        public void StartJumpingBarriers()
        {
            foreach (Barrier barrier in m_Barriers)
            {
                barrier.StartJumping();
            }
        }

        public void StopJumpingBarriers()
        {
            foreach (Barrier barrier in m_Barriers)
            {
                barrier.StopJumping();
            }
        }
    }
}
