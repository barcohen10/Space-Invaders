﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Infrastructure.ServiceInterfaces;

namespace SpaceInvaders.ObjectModel
{
    public class MouseSprite : Sprite
    {
        private IInputManager m_InputManager;
        private bool m_IsActive = false;

        public MouseSprite(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen)
        {
        }

        public bool IsActive
        {
            get
            {
                return m_IsActive;
            }

            set
            {
                m_IsActive = value;
                if (m_IsActive)
                {
                    TintColor = Color.LimeGreen;
                }
                else
                {
                    TintColor = Color.White;
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
        }

        public override void Update(GameTime gameTime)
        {
            bool isHover = IsMouseHover(m_InputManager);
            if (isHover && m_InputManager.MouseState.LeftButton == ButtonState.Pressed)
            {
                IsActive = !IsActive;
                Mouse.SetPosition((int)(this.Position.X + this.Width + (this.Width / 2)), (int)(this.Position.Y + (this.Height / 2)));
            }
            else if (isHover && !IsActive)
            {
                TintColor = Color.LimeGreen;
            }
            else if (!IsActive)
            {
                TintColor = Color.White;
            }

            base.Update(gameTime);
        }
    }
}
