using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class MouseSprite : Sprite
    {
        private IInputManager m_InputManager;
        private bool m_IsSelected = false;
        private List<string> m_TooltipMessages = new List<string>() { "Click here to use menu with mouse", "Click here to use menu with keyboard" };
        private Text m_Tooltip;

        public MouseSprite(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen)
        {
            m_Tooltip = SpritesFactory.CreateSprite(this.GameScreen, SpritesFactory.eSpriteType.SmallText) as Text;
        }

        public bool IsSelected
        {
            get
            {
                return m_IsSelected;
            }

            set
            {
                m_IsSelected = value;
                if (m_IsSelected)
                {
                    TintColor = Color.LimeGreen;
                    m_Tooltip.TextString = m_TooltipMessages[1];
                }
                else
                {
                    TintColor = Color.White;
                    m_Tooltip.TextString = m_TooltipMessages[0];
                }
            }
        }

        public override void Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            TintColor = Color.White;
            Scales *= new Vector2(0.7f);
            Position = Vector2.Zero;
            base.Initialize();
            m_Tooltip.TextString = m_TooltipMessages[0];
            m_Tooltip.Position = new Vector2(this.Position.X, this.Position.Y + this.Height + 20);
            m_Tooltip.Visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            bool isHover = IsMouseHover(m_InputManager);
            if (isHover && m_InputManager.MouseState.LeftButton == ButtonState.Pressed)
            {
                IsSelected = !IsSelected;
                Mouse.SetPosition((int)(this.Position.X + this.Width + (this.Width / 2)), (int)(this.Position.Y + (this.Height / 2)));
            }
            else if (isHover && !IsSelected)
            {
                TintColor = Color.LimeGreen;
            }
            else if (!IsSelected)
            {
                TintColor = Color.White;
            }

            if (isHover)
            {
                m_Tooltip.Visible = true;
            }
            else
            {
                m_Tooltip.Visible = false;
            }

            base.Update(gameTime);
        }
    }
}
