using C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems;
using C15Ex03Dotan301810610Bar308000322.ObjectModel;
using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu
{
    public abstract class MenuScreen : GameScreen
    {
        private Menu m_Menu;
        private string m_MenuTitle;
        private Text m_TitleText;
        private int m_ActiveMenuItemIndex = -1;
        private MouseSprite m_Mouse;

        public MenuScreen(Game i_Game, string i_MenuTitle)
            : base(i_Game)
        {
            m_Menu = new Menu(i_MenuTitle);
            m_MenuTitle = i_MenuTitle;
            this.Game.IsMouseVisible = true;
        }

        protected Menu Menu { get { return m_Menu; } set { m_Menu = value; } }

        protected Text TitleText { get { return m_TitleText; } set { m_TitleText = value; } }

        protected abstract void InitMenuItems();

        protected void AddMenuItem(MenuItem menuItem)
        {
            GameMenuItem menuItemGame = menuItem as GameMenuItem;
            if (menuItemGame != null)
            {
                float y = TitleText.Position.Y + TitleText.Height + TitleText.Height / 2;
                if (m_Menu.MenuItems != null)
                {
                    GameMenuItem lastItem = (m_Menu[m_Menu.Count - 1] as GameMenuItem);
                    if (lastItem != null)
                    {
                        y = lastItem.Text.Position.Y + lastItem.Text.Height + lastItem.Text.Height / 2;
                    }
                }
                menuItemGame.Position = new Vector2(0, y);
                this.Menu.AddMenuItem(menuItemGame);
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
            m_Mouse = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.Mouse) as C15Ex03Dotan301810610Bar308000322.ObjectModel.MouseSprite;
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
                if (m_Mouse.IsActive)
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

        private void activateMenuItem()
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

        private void selectMenuItem()
        {
            GameMenuItem activeMenuItem = m_Menu[m_ActiveMenuItemIndex] as GameMenuItem;

            if (activeMenuItem != null)
            {
                activeMenuItem.IsSelected = true;
                Menu[m_ActiveMenuItemIndex].RunMethod(Keys.Enter);

                for (int i = 0; i < m_Menu.Count; i++)
                {
                    if (i != m_ActiveMenuItemIndex)
                    {
                        activeMenuItem = m_Menu[i] as GameMenuItem;
                        activeMenuItem.IsSelected = false;
                    }
                }
            }
        }

        private void handleMouse()
        {
            bool isMouseHover = isMouseHoverMenuItem();
            if (isMouseHover && InputManager.MouseState.LeftButton == ButtonState.Pressed)
            {
                selectMenuItem();
            }
            else if (m_ActiveMenuItemIndex > -1)
            {
                GameMenuItem item = (Menu[m_ActiveMenuItemIndex] as GameMenuItem);
                if (isMouseHover && !item.IsSelected)
                {
                    activateMenuItem();
                }
            }

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
                m_Menu[m_ActiveMenuItemIndex].RunMethod(Keys.Enter);
            }

            if (m_ActiveMenuItemIndex > -1)
            {
                ToggleMenuItem item = (m_Menu[m_ActiveMenuItemIndex] as ToggleMenuItem);
                activateMenuItem();
                if (item != null)
                {
                    if (InputManager.KeyPressed(item.MethodAndKeys[0].ActivateKey))
                    {
                        item.DownOption();
                    }
                    else if (InputManager.KeyPressed(item.MethodAndKeys[1].ActivateKey))
                    {
                        item.UpOption();
                    }
                }
            }
        }


    }
}
