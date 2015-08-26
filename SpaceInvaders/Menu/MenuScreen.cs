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
        private int m_ActiveMenuItemIndex = 0;

        public MenuScreen(Game i_Game, string i_MenuTitle) : base(i_Game)
        {
            m_Menu = new Menu(i_MenuTitle);
            m_MenuTitle = i_MenuTitle;
        }

        protected Menu Menu { get { return m_Menu; } set { m_Menu = value; } }

        protected Text TitleText { get { return m_TitleText; } set { m_TitleText = value; } }

        protected abstract void InitMenuItems();

        protected void AddMenuItem(MenuItem menuItem)
        {
            GameMenuItem menuItemGame = menuItem as GameMenuItem;
            if(menuItemGame != null)
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

        public override void Initialize()
        {
            m_TitleText = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
            m_TitleText.TextString = m_MenuTitle;
            m_TitleText.Position = new Vector2(0, 20);
            TextServices.CenterTextsOnScreen(this, new List<Text>() { m_TitleText });
            InitMenuItems();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(m_Menu.Count > 0)
            {
                if (InputManager.KeyPressed(Keys.Up))
                {
                    m_ActiveMenuItemIndex = (m_ActiveMenuItemIndex - 1) >= 0 ? (m_ActiveMenuItemIndex - 1) : (Menu.Count - 1);
                }
                if (InputManager.KeyPressed(Keys.Down))
                {
                    m_ActiveMenuItemIndex = (m_ActiveMenuItemIndex + 1) <= (Menu.Count - 1) ? (m_ActiveMenuItemIndex + 1) : 0;
                }
                if (InputManager.KeyPressed(Keys.Enter))
                {
                    m_Menu[m_ActiveMenuItemIndex].RunMethod(Keys.Enter);
                }

                GameMenuItem activeMenuItem = m_Menu[m_ActiveMenuItemIndex] as GameMenuItem;

                if (activeMenuItem != null)
                {
                    activeMenuItem.IsActive = true;
                }

                for(int i = 0; i < m_Menu.Count; i++)
                {
                    if(i != m_ActiveMenuItemIndex)
                    {
                        activeMenuItem = m_Menu[i] as GameMenuItem;
                        activeMenuItem.IsActive = false;
                    }
                }

            }
            base.Update(gameTime);
        }

    }
}
