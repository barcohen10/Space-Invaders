using SpaceInvaders.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Menus
{
    /// <summary>
    /// Dynamic menu (Delegates)
    /// </summary>
    public class Menu
    {
        private readonly string r_MenuTitle = "Menu";
        private List<MenuItem> m_MenuItems = null;

        public Menu(string i_MenuTitle)
        {
            r_MenuTitle = i_MenuTitle;
        }

        public MenuItem this[string i_ItemName]
        {
            get
            {
                int indexOfMenuItem = getIndexOfMenuItem(i_ItemName);
                return m_MenuItems[indexOfMenuItem];
            }
        }

        public MenuItem this[int i_Index]
        {
            get
            {
                return m_MenuItems[i_Index];
            }
        }

        public int Count
        {
            get
            {
                return m_MenuItems.Count;
            }
        }

        public string MenuTitle
        {
            get { return r_MenuTitle; }
        }

        public List<MenuItem> MenuItems
        {
            get { return m_MenuItems; }
            set { m_MenuItems = value; }
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            if (m_MenuItems == null)
            {
                m_MenuItems = new List<MenuItem>();
            }

            m_MenuItems.Add(i_MenuItem);
        }

        private int getIndexOfMenuItem(string i_ItemName)
        {
            int indexOfItem = 0;
            bool IsMenuItemFound = false;
            foreach (MenuItem menuItem in m_MenuItems)
            {
                if (menuItem.ItemName.Equals(i_ItemName))
                {
                    IsMenuItemFound = true;
                    break;
                }

                indexOfItem++;
            }

            if (!IsMenuItemFound)
            {
                string errorMessage = string.Format("Error! Menu item '{0}' Not Exits!", i_ItemName);
                throw new Exception(errorMessage);
            }

            return indexOfItem;
        }
    }
}
