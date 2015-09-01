using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Services;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;

namespace SpaceInvaders.Menus
{
    public class GameMenuItem : MenuItem
    {
        private readonly Color r_OriginalColor;
        private Text m_Text;
        private bool m_IsActive = false, m_IsSelected = false;
        protected GameScreen m_GameScreen;
        protected SpritesFactory.eSpriteType m_TextSpriteType = SpritesFactory.eSpriteType.BigText;
        private Sound m_MoveBetweenItemsSound;
        
        public enum eFontSize
        {
            Small,
            Medium,
            Big
        }

        public GameMenuItem(string i_ItemName, GameScreen i_GameScreen, Color i_Color, eFontSize i_FontSize = eFontSize.Big, params MethodKey[] i_Methods)
            : base(i_ItemName, i_Methods)
        {
            switch(i_FontSize)
            {
                case eFontSize.Medium:
                    m_TextSpriteType = SpritesFactory.eSpriteType.MediumText;
                    break;
                case eFontSize.Small:
                    m_TextSpriteType = SpritesFactory.eSpriteType.SmallText;
                    break;
            }

            m_Text = SpritesFactory.CreateSprite(i_GameScreen, m_TextSpriteType) as Text;
            m_Text.TextString = i_ItemName;
            m_Text.TintColor = r_OriginalColor = i_Color;
            m_GameScreen = i_GameScreen;
            m_MoveBetweenItemsSound = SoundFactory.CreateSound(this.m_GameScreen, SoundFactory.eSoundType.MenuMove);
        }

        public Text Text
        { 
            get
            {
                return m_Text;
            } 

            set 
            { 
                m_Text = value; 
            } 
        }

        public string TextString 
        { 
            get
            {
                return m_Text.TextString;
            } 

            set
            { 
                m_Text.TextString = value;
            } 
        }

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

        public virtual float Width
        {
            get
            {
                return Text.Width;
            }
        }

        public virtual bool IsActive
        {
            get { return m_IsActive; }
            set
            {
                if (value && !m_IsActive)
                {
                    activateMenuItem();
                }
                else if (!m_IsSelected && !value)
                {
                    deactivateMenuItem();
                }

                m_IsActive = value;
            }
        }

        protected void activateMenuItem()
        {
            m_Text.TintColor = Color.Red;
            m_MoveBetweenItemsSound.Play();
            m_Text.Animations["pulse"].Reset();
            m_Text.Animations["pulse"].Resume();
        }

        private void deactivateMenuItem()
        {
            m_Text.TintColor = r_OriginalColor;
            m_Text.Animations["pulse"].Reset();
            m_Text.Animations["pulse"].Pause();
        }
    }
}
