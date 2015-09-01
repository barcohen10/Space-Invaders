using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Menus.ConcreteMenuItems.RangeMenuItem
{
    public class RangeMenuItem : GameMenuItem
    {
        private Range m_Range;
        private MethodKey m_DecreaseMethod, m_IncreaseMethod;

        public RangeMenuItem(string i_ItemName, GameScreen i_GameScreen, int i_Value, int i_Min, int i_Max, int i_Jump, MethodKey i_DecreaseMethod, MethodKey i_IncreaseMethod)
            : base(i_ItemName, i_GameScreen, Color.White, GameMenuItem.eFontSize.Medium, new MethodKey[] { i_DecreaseMethod, i_IncreaseMethod })
        {
            float x = this.Text.Width + this.Text.Position.X + 20;
            m_Range = new Range(i_GameScreen, i_Value, i_Min, i_Max, i_Jump);
            m_Range.Initialize();
            m_Range.Position = new Vector2(x, this.Position.Y);
            m_DecreaseMethod = i_DecreaseMethod;
            m_IncreaseMethod = i_IncreaseMethod;
        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }

            set
            {
                float addToX = value.X - base.Position.X;
                float addToY = value.Y - base.Position.Y;
                base.Position = value;
                m_Range.Position = new Vector2(m_Range.Position.X + addToX, m_Range.Position.Y + addToY);
            }
        }

        public MethodKey DecreaseMethod 
        { 
            get 
            {
                return m_DecreaseMethod;
            }
        }

        public MethodKey IncreaseMethod
        { 
            get
            { 
                return m_IncreaseMethod;
            } 
        }

        public void IncreaseJump()
        {
            m_Range.IncreaseJump();
            this.RunMethod(m_IncreaseMethod.ActivateKey);
        }

        public void DecreaseJump()
        {
            m_Range.DecreaseJump();
            this.RunMethod(m_DecreaseMethod.ActivateKey);
        }
    }
}
