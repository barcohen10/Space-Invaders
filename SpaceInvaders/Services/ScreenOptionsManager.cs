using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Services
{
    public class ScreenOptionsManager 
    {
        private Game m_Game;
        private GraphicsDeviceManager m_GraphicsDeviceManager;
        public ScreenOptionsManager(Game i_Game)
        {
            m_Game = i_Game;
            m_GraphicsDeviceManager = i_Game.Services.GetService(typeof(GraphicsDeviceManager)) as GraphicsDeviceManager;
        }
        public void ToggleMouseVisibility()
        {
            m_Game.IsMouseVisible = !m_Game.IsMouseVisible;
        }
        public void ToggleFullScreenMode()
        {
            m_GraphicsDeviceManager.ToggleFullScreen();
        }
        public void ToggleAllowWindowResizing()
        {
            m_Game.Window.AllowUserResizing = !m_Game.Window.AllowUserResizing;
        }
    }
}
