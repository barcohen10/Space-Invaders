﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;
using SpaceInvaders.Menus.ConcreteMenuItems.RangeMenuItem;
using SpaceInvaders.Menus.ConcreteMenuItems;
using SpaceInvaders.Infrastructure.ObjectModel.Menu;

namespace SpaceInvaders.Menus
{
    public abstract class MenuScreen : GameScreen
    {
        private Menu m_Menu;
        private string m_MenuTitle;
        private Text m_TitleText;
        private int m_ActiveMenuItemIndex = -1;
        private MouseSprite m_Mouse;
        private ButtonState m_LastBTNState = ButtonState.Released;
        private int m_LastMouseWheelValue = 0;

        public MenuScreen(Game i_Game, string i_MenuTitle)
            : base(i_Game)
        {
            m_Menu = new Menu(i_MenuTitle);
            m_MenuTitle = i_MenuTitle;
        }

        protected Menu Menu
        { 
            get
            { 
                return m_Menu;
            } 

            set 
            { 
                m_Menu = value;
            } 
        }

        protected Text TitleText 
        {
            get
            { 
                return m_TitleText;
            }

            set
            { 
                m_TitleText = value;
            } 
        }

        protected abstract void InitMenuItems();

        protected void AddMenuItems(params MenuItem[] i_MenuItems)
        {
            foreach (MenuItem menuItem in i_MenuItems)
            {
                GameMenuItem menuItemGame = menuItem as GameMenuItem;
                if (menuItemGame != null)
                {
                    float y = TitleText.Position.Y + TitleText.Height + (TitleText.Height / 1.5f);
                    if (m_Menu.MenuItems != null)
                    {
                        GameMenuItem lastItem = m_Menu[m_Menu.Count - 1] as GameMenuItem;
                        if (lastItem != null)
                        {
                            y = lastItem.Text.Position.Y + lastItem.Text.Height + (lastItem.Text.Height / 1.5f);
                        }
                    }

                    menuItemGame.Position = new Vector2(0, y);
                    this.Menu.AddMenuItem(menuItemGame);
                }
            }
        }

        private bool isMouseHoverMenuItem()
        {
            bool isMouseCollided = false;
            for (int i = 0; i < Menu.MenuItems.Count; i++)
            {
                if ((Menu.MenuItems[i] as GameMenuItem).Text.IsMouseHover(InputManager))
                {
                    m_ActiveMenuItemIndex = i;
                    isMouseCollided = true;
                    break;
                }
            }

            return isMouseCollided;
        }

        public override void Initialize()
        {
            m_Mouse = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.Mouse) as MouseSprite;
            m_TitleText = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
            m_TitleText.TextString = m_MenuTitle;
            m_TitleText.Position = new Vector2(0, 20);
            TextServices.CenterTextsOnScreen(this, new List<Text>() { m_TitleText });
            InitMenuItems();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (m_Menu.Count > 0)
            {
                if (m_Mouse.IsSelected)
                {
                    handleMouse();
                }
                else
                {
                    handleKeyboard();
                }
            }

            base.Update(gameTime);
        }

        private void activateCurrentMenuItem()
        {
            GameMenuItem activeMenuItem = m_Menu[m_ActiveMenuItemIndex] as GameMenuItem;

            if (activeMenuItem != null)
            {
                activeMenuItem.IsActive = true;

                for (int i = 0; i < m_Menu.Count; i++)
                {
                    if (i != m_ActiveMenuItemIndex)
                    {
                        activeMenuItem = m_Menu[i] as GameMenuItem;
                        activeMenuItem.IsActive = false;
                    }
                }
            }
        }

        private void runMenuItemMethod()
        {
            GameMenuItem activeMenuItem = m_Menu[m_ActiveMenuItemIndex] as GameMenuItem;

            if (activeMenuItem != null)
            {
                Menu[m_ActiveMenuItemIndex].RunMethod(Keys.Enter);
            }
        }

        private void handleMouse()
        {
            bool isMouseHover = isMouseHoverMenuItem();
            if (isMouseHover && InputManager.MouseState.LeftButton == ButtonState.Pressed && m_LastBTNState == ButtonState.Released)
            {
                runMenuItemMethod();
            }
            else if (m_ActiveMenuItemIndex > -1)
            {
                GameMenuItem item = Menu[m_ActiveMenuItemIndex] as GameMenuItem;
                if (isMouseHover && !item.IsActive)
                {
                    activateCurrentMenuItem();
                }
                else if (isMouseHover)
                {
                    RangeMenuItem rangeItem = m_Menu[m_ActiveMenuItemIndex] as RangeMenuItem;
                    if (rangeItem != null)
                    {
                        if (InputManager.MouseState.ScrollWheelValue > m_LastMouseWheelValue)
                        {
                            rangeItem.IncreaseJump();
                        }
                        else if (InputManager.MouseState.ScrollWheelValue < m_LastMouseWheelValue)
                        {
                            rangeItem.DecreaseJump();
                        }

                        m_LastMouseWheelValue = InputManager.MouseState.ScrollWheelValue;
                    }
                }
            }

            m_LastBTNState = InputManager.MouseState.LeftButton;
        }

        private void handleKeyboard()
        {
            if (InputManager.KeyPressed(Keys.Up))
            {
                m_ActiveMenuItemIndex = (m_ActiveMenuItemIndex - 1) >= 0 ? (m_ActiveMenuItemIndex - 1) : (Menu.Count - 1);
            }
            else if (InputManager.KeyPressed(Keys.Down))
            {
                m_ActiveMenuItemIndex = (m_ActiveMenuItemIndex + 1) <= (Menu.Count - 1) ? (m_ActiveMenuItemIndex + 1) : 0;
            }
            else if (InputManager.KeyPressed(Keys.Enter))
            {
                if (m_ActiveMenuItemIndex >= 0)
                {
                    m_Menu[m_ActiveMenuItemIndex].RunMethod(Keys.Enter);
                }
            }

            if (m_ActiveMenuItemIndex > -1)
            {
                activateCurrentMenuItem();
                ToggleMenuItem toggleItem = m_Menu[m_ActiveMenuItemIndex] as ToggleMenuItem;
                if (toggleItem != null)
                {
                    if (InputManager.KeyPressed(toggleItem.ToggleRightMethod.ActivateKey))
                    {
                        toggleItem.ToggleRight();
                    }
                    else if (InputManager.KeyPressed(toggleItem.ToggleLeftMethod.ActivateKey))
                    {
                        toggleItem.ToggleLeft();
                    }
                }
                else
                {
                    RangeMenuItem rangeItem = m_Menu[m_ActiveMenuItemIndex] as RangeMenuItem;
                    if (rangeItem != null)
                    {
                        if (InputManager.KeyPressed(rangeItem.DecreaseMethod.ActivateKey))
                        {
                            rangeItem.DecreaseJump();
                        }
                        else if (InputManager.KeyPressed(rangeItem.IncreaseMethod.ActivateKey))
                        {
                            rangeItem.IncreaseJump();
                        }
                    }
                }
            }
        }

        protected void done()
        {
            GameScreen previousScreen = ScreensManager.ActiveScreen.PreviousScreen;
            ScreensManager.Remove(ScreensManager.ActiveScreen);
            ScreensManager.SetCurrentScreen(previousScreen);
        }
    }
}
