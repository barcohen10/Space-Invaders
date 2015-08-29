using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems.RangeMenuItem
{
    public class RangeMenuItem : GameMenuItem
    {
        private Range m_Range;
        private MethodKey[] m_Methods;

        public RangeMenuItem(string i_ItemName, GameScreen i_GameScreen, int i_Value, int i_Min, int i_Max, int i_Jump, params MethodKey[] i_Methods)
            : base(i_ItemName, i_GameScreen, Color.White, GameMenuItem.eFontSize.Medium, i_Methods)
        {
            float x = this.Text.Width + this.Text.Position.X + 20;
            m_Range = new Range(i_GameScreen, i_Value, i_Min, i_Max, i_Jump);
            m_Range.Initialize();
            m_Range.Position = new Vector2(x, this.Position.Y);
            m_Methods = i_Methods;
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


    }
}
