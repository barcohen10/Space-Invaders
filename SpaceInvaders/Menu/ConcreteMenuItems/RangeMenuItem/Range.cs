using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems.RangeMenuItem
{
    public class Range : GameComponent
    {
        private const string k_BarAsset = @"Sprites\Bar";
        private const string k_RectangleAsset = @"Sprites\Rectangle";
        private int m_Min, m_Max, m_Jump, m_Value, m_TotalJumps = 0;
        private Bar m_Bar;
        private Rectangle m_Rectangle;
        private GameScreen m_GameScreen;
        private float m_DistanceToJump;

        public Range(GameScreen i_GameScreen, int i_Value, int i_Min, int i_Max, int i_Jump) : base(i_GameScreen.Game)
        {
            m_GameScreen = i_GameScreen;
            m_Value = i_Value;
            m_Min = i_Min;
            m_Max = i_Max;
            m_Jump = i_Jump;
        }

        public override void Initialize()
        {
            base.Initialize();
            initRangeComponent();
        }

        public Vector2 Position
        {
           set
            {
               m_Rectangle.Position = value;
               m_Bar.Position = new Vector2(m_Rectangle.Position.X + (m_TotalJumps * m_DistanceToJump), m_Rectangle.Position.Y - m_Bar.Height / 3);
            }
        }

        public void AddJump()
        {
            MathHelper.Clamp(m_Value + m_Jump, m_Min, m_Max);
            m_TotalJumps++;
            m_Bar.Position = new Vector2(m_Bar.Position.X + m_DistanceToJump, m_Bar.Position.Y);
        }

        public void DecreaseJump()
        {
            MathHelper.Clamp(m_Value - m_Jump, m_Min, m_Max);
            m_TotalJumps--;
            m_Bar.Position = new Vector2(m_Bar.Position.X - m_DistanceToJump, m_Bar.Position.Y);
        }

        private void initRangeComponent()
        {
            m_Rectangle = new Rectangle(m_GameScreen, k_RectangleAsset);
            m_Bar = new Bar(m_GameScreen, k_BarAsset);
            m_Rectangle.Initialize();
            m_Bar.Initialize();
            m_Rectangle.Position = new Vector2(m_Rectangle.Position.X, m_Bar.Height / 3);
            m_DistanceToJump = m_Rectangle.Width / (m_Max / m_Jump);
            m_GameScreen.Add(m_Rectangle);
            m_GameScreen.Add(m_Bar);
        }
    }
}
