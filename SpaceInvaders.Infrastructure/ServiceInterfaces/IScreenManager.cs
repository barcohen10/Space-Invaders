using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Infrastructure.ServiceInterfaces
{
    public interface IScreensManager
    {
        GameScreen ActiveScreen 
        {
            get; 
        }

        void SetCurrentScreen(GameScreen i_NewScreen);

        bool Remove(GameScreen i_Screen);

        void Add(GameScreen i_Screen);
    }
}
