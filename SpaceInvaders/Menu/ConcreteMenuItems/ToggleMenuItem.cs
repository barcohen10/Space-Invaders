using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems
{
    public class ToggleMenuItem : GameMenuItem
    {
        private List<Text> m_Options = new List<Text>();
        private int m_SelectedOptionIndex = 0;
        private Text m_Separator;
        private MethodKey m_ToggleRightMethod, m_ToggleLeftMethod;

        public ToggleMenuItem(string i_ItemName, GameScreen i_GameScreen, List<string> i_Options, Keys i_ActivateItemKey, MethodKey i_ToggleRightMethod, MethodKey i_ToggleLeftMethod)
            : base(i_ItemName, i_GameScreen, Color.White, GameMenuItem.eFontSize.Medium, new MethodKey[]{i_ToggleRightMethod, i_ToggleLeftMethod})
        {
            if (i_Options.Count > 0)
            {
                initOptions(i_Options);
                m_Options[m_SelectedOptionIndex].TintColor = Color.Aqua;
                m_ToggleRightMethod = i_ToggleRightMethod;
                m_ToggleLeftMethod = i_ToggleLeftMethod;
            }
        }

        public List<Text> Options { get { return m_Options; } }

        public MethodKey ToggleRightMethod { get { return m_ToggleRightMethod; } }

        public MethodKey ToggleLeftMethod { get { return m_ToggleLeftMethod; } }

        private void initOptions(List<string> i_Options)
        {
            float x = this.Text.Width + this.Text.Position.X + 20;
            Text optionText = null;
            for (int i = 0; i < 2; i++ )
            {
                optionText = SpritesFactory.CreateSprite(m_GameScreen, m_TextSpriteType) as Text;
                optionText.TextString = i_Options[i];
                optionText.Position = new Vector2(x, this.Position.Y);
                m_Options.Add(optionText);
                x = optionText.Width + optionText.Position.X + 30;
            }

            m_Separator = SpritesFactory.CreateSprite(m_GameScreen, m_TextSpriteType) as Text;
            x = optionText.Position.X - 15;
            m_Separator.TextString = "/";
            m_Separator.Position = new Vector2(x, this.Position.Y);
            optionText.Position = new Vector2(m_Separator.Position.X + m_Separator.Width + 15, this.Position.Y);

            m_Options[0].RightMouseClick += ToggleLeft;
            m_Options[1].RightMouseClick += ToggleRight;
        }

        private void ToggleRight(object sender, EventArgs args)
        {
            ToggleRight();
        }

        public void ToggleRight()
        {
            if (IsActive)
            {
                m_Options[m_SelectedOptionIndex].TintColor = Color.White;
                m_SelectedOptionIndex = 1;
                m_Options[m_SelectedOptionIndex].TintColor = Color.Aqua;
                this.RunMethod(m_ToggleRightMethod.ActivateKey);
            }
        }

        public void ToggleLeft()
        {
            if (IsActive)
            {
                m_Options[m_SelectedOptionIndex].TintColor = Color.White;
                m_SelectedOptionIndex = 0;
                m_Options[m_SelectedOptionIndex].TintColor = Color.Aqua;
                this.RunMethod(m_ToggleLeftMethod.ActivateKey);
            }
        }

        private void ToggleLeft(object sender, EventArgs args)
        {
            ToggleLeft();
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
                foreach(Text option in m_Options)
                {
                    option.Position = new Vector2(option.Position.X + addToX, option.Position.Y + addToY);
                }
                m_Separator.Position = new Vector2(m_Separator.Position.X + addToX, m_Separator.Position.Y + addToY);
            }
        }


    }
}
