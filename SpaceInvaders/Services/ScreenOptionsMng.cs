using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Services
{
    public class ScreenOptionsMng 
    {
        private Game m_Game;
        private GraphicsDeviceManager m_GraphicsDeviceManager;

        public ScreenOptionsMng(Game i_Game)
        {
            m_Game = i_Game;
            m_GraphicsDeviceManager = i_Game.Services.GetService(typeof(GraphicsDeviceManager)) as GraphicsDeviceManager;
        }

        public string MouseVisible 
        { 
            get 
            { 
                string result = "Invisible";

                if( m_Game.IsMouseVisible)
                {
                    result = "Visible";
                }
                return result;
            } 
        }

        public string FullScreenMode
        {
            get
            {
                string result = "Off";

                if (m_GraphicsDeviceManager.IsFullScreen)
                {
                    result = "On";
                }
                return result;
            }
        }

        public string AllowWindowResizingMode
        {
            get
            {
                string result = "Off";

                if (m_Game.Window.AllowUserResizing)
                {
                    result = "On";
                }
                return result;
            }
        }

        public void MouseVisibilityOn()
        {
            m_Game.IsMouseVisible = true;
        }

        public void MouseVisibilityOff()
        {
            m_Game.IsMouseVisible = false;
        }

        public void FullScreenOn()
        {
            if(!m_GraphicsDeviceManager.IsFullScreen)
            {
                m_GraphicsDeviceManager.ToggleFullScreen();
            }
        }

        public void FullScreenOff()
        {
            if (m_GraphicsDeviceManager.IsFullScreen)
            {
                m_GraphicsDeviceManager.ToggleFullScreen();
            }
        }

        public void AllowWindowResizing()
        {
            m_Game.Window.AllowUserResizing = true;
        }

        public void DisallowWindowResizing()
        {
            m_Game.Window.AllowUserResizing = false;
        }
    }
}
