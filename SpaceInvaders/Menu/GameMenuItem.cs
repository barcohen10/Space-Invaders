using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu
{
    public class GameMenuItem : MenuItem
    {
        private Text m_Text;
        private bool m_IsActive = false;
        private readonly Color r_OriginalColor;

        public GameMenuItem(string i_ItemName, GameScreen i_GameScreen, Color i_Color, params MethodKey[] i_Methods)
            : base(i_ItemName, i_Methods)
        {
            m_Text = SpritesFactory.CreateSprite(i_GameScreen, SpritesFactory.eSpriteType.BigText) as Text;
            m_Text.TextString = i_ItemName;
            m_Text.TintColor = r_OriginalColor = i_Color;
        }

        public Text Text { get { return m_Text; } set { m_Text = value; } }

        public string TextString { get { return m_Text.TextString; } set { m_Text.TextString = value; } }

        public virtual Vector2 Position
        {
            get
            {
                return Text.Position;
            }
            set
            {
                Text.Position = value;
            }
        }

        public bool IsActive 
        { 
            get { return m_IsActive; } 
            set 
            {
                m_IsActive = value;
                if(m_IsActive)
                {
                    activateMenuItem();
                }
                else
                {
                    deactivateMenuItem();
                }
            } 
        }

        private void activateMenuItem()
        {
            m_Text.TintColor = Color.Red;
        }

        private void deactivateMenuItem()
        {
            m_Text.TintColor = r_OriginalColor;
        }

    }
}
