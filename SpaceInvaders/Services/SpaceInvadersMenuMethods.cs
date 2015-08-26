using C15Ex03Dotan301810610Bar308000322.Interfaces;
using C15Ex03Dotan301810610Bar308000322.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.Managers;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Services
{
    public class SpaceInvadersMenuMethods : GameComponent, ISpaceInvaders, IAction
    {
        private readonly eMethodsToRun r_MethodToRun;
        private static Game m_Game;

        public enum eMethodsToRun
        {
            Play,
            Quit
        }

        public SpaceInvadersMenuMethods(Game i_Game, eMethodsToRun i_MethodToRun)
            : base(i_Game)
        {
            r_MethodToRun = i_MethodToRun;
            m_Game = i_Game;
        }

        void ISpaceInvaders.Play()
        {
            ScreensManager screensManager = SpaceInvadersServices.GetScreensManagerComponent(m_Game);
            screensManager.SetCurrentScreen(new GamingScreen(m_Game));
            screensManager.SetCurrentScreen(new MoveStageScreen(m_Game, 1));
        }
        void ISpaceInvaders.Quit()
        {
            m_Game.Exit();
        }

        public void RunMethod()
        {
            switch (r_MethodToRun)
            {
                case eMethodsToRun.Play:
                    {
                        (this as ISpaceInvaders).Play();
                        break;
                    }
                case eMethodsToRun.Quit:
                    {
                        (this as ISpaceInvaders).Quit();
                        break;
                    }
            }
        }
    }

}
